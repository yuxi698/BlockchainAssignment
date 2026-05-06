using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainAssignment
{
    public partial class BlockchainApp : Form
    {
        // Global blockchain object
        private Blockchain blockchain;

        // Default App Constructor
        public BlockchainApp()
        {
            InitializeComponent();
            this.blockchain = new Blockchain();
            UpdateText("New blockchain initialised!");
        }

        /* PRINTING */
        private void UpdateText(String text)
        {
            output.Text = text;
        }

        private void ReadAll_Click(object sender, EventArgs e)
        {
            UpdateText(blockchain.ToString());
        }

        private void PrintBlock_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(blockNo.Text, out int index))
                UpdateText(blockchain.GetBlockAsString(index));
            else
                UpdateText("Invalid Block No.");
        }

        private void PrintPendingTransactions_Click(object sender, EventArgs e)
        {
            UpdateText(String.Join("\n", blockchain.transactionPool));
        }

        /* WALLETS */
        private void GenerateWallet_Click(object sender, EventArgs e)
        {
            Wallet.Wallet myNewWallet = new Wallet.Wallet(out string privKey);
            publicKey.Text = myNewWallet.publicID;
            privateKey.Text = privKey;
        }

        private void ValidateKeys_Click(object sender, EventArgs e)
        {
            if (Wallet.Wallet.ValidatePrivateKey(privateKey.Text, publicKey.Text))
                UpdateText("Keys are valid");
            else
                UpdateText("Keys are invalid");
        }

        private void CheckBalance_Click(object sender, EventArgs e)
        {
            UpdateText(blockchain.GetBalance(publicKey.Text).ToString() + " AssignmentCoin");
        }

        /* TRANSACTION MANAGEMENT */
        private void CreateTransaction_Click(object sender, EventArgs e)
        {
            Transaction transaction = new Transaction(
                publicKey.Text,
                receiver.Text,
                Double.Parse(amount.Text),
                Double.Parse(fee.Text),
                privateKey.Text);
            blockchain.transactionPool.Add(transaction);
            UpdateText(transaction.ToString());
        }

        /* BLOCK MANAGEMENT */
        /* BLOCK MANAGEMENT */
        // Conduct Proof-of-work in order to mine transactions from the pool and submit a new block to the Blockchain
        private void NewBlock_Click(object sender, EventArgs e)
        {
            // Retrieve pending transactions to be added to the newly generated Block
            List<Transaction> transactions = blockchain.GetPendingTransactions();

            // Use the blockchain's current (adaptive) difficulty
            int diffUsed = blockchain.currentDifficulty;
            DateTime startTime = DateTime.Now;
            Block newBlock = new Block(blockchain.GetLastBlock(), transactions, publicKey.Text, diffUsed);
            blockchain.blocks.Add(newBlock);
            double minedInSeconds = (DateTime.Now - startTime).TotalSeconds;

            // Adaptive difficulty: recompute for next block based on recent block times
            string adaptiveStatus = blockchain.RecomputeDifficulty();

            // Show chain + adaptive info at top
            string output = "Mined block #" + newBlock.Index + " at difficulty " + diffUsed
                + " in " + minedInSeconds.ToString("F2") + "s\n"
                + adaptiveStatus + "\n\n"
                + blockchain.ToString();
            UpdateText(output);
        }

        /* ===== Part 6 Task 3: Mining Preference Strategies ===== */
        private void MineWithPreference(MinePreference pref)
        {
            if (string.IsNullOrEmpty(publicKey.Text))
            {
                UpdateText("Please generate a wallet first (publicKey will be the miner).");
                return;
            }

            List<Transaction> transactions = blockchain.SelectTransactions(pref, publicKey.Text);
            int diffUsed = blockchain.currentDifficulty;
            Block newBlock = new Block(blockchain.GetLastBlock(), transactions, publicKey.Text, diffUsed);
            blockchain.blocks.Add(newBlock);
            string adaptiveStatus = blockchain.RecomputeDifficulty();

            string outStr = "Mined block #" + newBlock.Index
                + " using strategy: " + pref
                + " | difficulty " + diffUsed
                + " | mine time " + newBlock.mineDurationSeconds.ToString("F3") + "s\n"
                + adaptiveStatus + "\n\n"
                + blockchain.ToString();
            UpdateText(outStr);
        }

        private void MineGreedy_Click(object sender, EventArgs e) { MineWithPreference(MinePreference.Greedy); }
        private void MineAltruistic_Click(object sender, EventArgs e) { MineWithPreference(MinePreference.Altruistic); }
        private void MineRandom_Click(object sender, EventArgs e) { MineWithPreference(MinePreference.Random); }
        private void MineAddrPref_Click(object sender, EventArgs e) { MineWithPreference(MinePreference.AddressPreference); }

        private void Compare_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(publicKey.Text))
            {
                UpdateText("Please generate a wallet first (used for AddressPreference logic).");
                return;
            }
            UpdateText(blockchain.CompareAllStrategies(publicKey.Text));
        }

        /* BLOCKCHAIN VALIDATION */
        private void Validate_Click(object sender, EventArgs e)
        {
            if (blockchain.blocks.Count == 1)
            {
                if (!Blockchain.ValidateHash(blockchain.blocks[0]))
                    UpdateText("Blockchain is invalid");
                else
                    UpdateText("Blockchain is valid");
                return;
            }

            for (int i = 1; i < blockchain.blocks.Count; i++)
            {
                if (
                    blockchain.blocks[i].prevHash != blockchain.blocks[i - 1].hash ||
                    !Blockchain.ValidateHash(blockchain.blocks[i]) ||
                    !Blockchain.ValidateMerkleRoot(blockchain.blocks[i])
                )
                {
                    UpdateText("Blockchain is invalid");
                    return;
                }
            }
            UpdateText("Blockchain is valid");
        }
        /* ===== Part 6 Task 1: Multi-threaded Mining Benchmark ===== */
        private void Benchmark_Click(object sender, EventArgs e)
        {
            int[] threadCounts = { 1, 2, 4, 8 };
            int[] difficulties = { 4, 5 };

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== MULTI-THREADED MINING BENCHMARK ===");
            sb.AppendLine("(e-nonce stride: thread i tests nonces {i, i+N, i+2N, ...})");
            sb.AppendLine();

            foreach (int diff in difficulties)
            {
                // Same baseInput across all thread counts at this difficulty -> fair comparison
                string baseInput = "BENCH_d" + diff + "_" + DateTime.Now.Ticks;
                sb.AppendLine("--- Difficulty " + diff + " (target: " + new string('0', diff) + "...) ---");
                sb.AppendLine("Threads | Time (ms) | Nonce found | Hash prefix");
                sb.AppendLine("--------+-----------+-------------+------------");

                foreach (int threads in threadCounts)
                {
                    // Show progress so user knows it's working
                    output.Text = sb.ToString() + Environment.NewLine + "Running with " + threads + " thread(s)...";
                    Application.DoEvents();

                    var r = Block.RunBenchmark(diff, threads, baseInput);
                    string hashPrefix = (r.hash ?? "").Substring(0, Math.Min(12, (r.hash ?? "").Length));
                    sb.AppendLine(string.Format("  {0,5} | {1,9} | {2,11} | {3}",
                        threads, r.elapsedMs, r.nonce, hashPrefix));
                }
                sb.AppendLine();
            }

            sb.AppendLine("Note: Speedup from threading shows clearer at higher difficulty.");
            sb.AppendLine("At low difficulty, thread management overhead may dominate.");

            UpdateText(sb.ToString());
        }
        // === Designer 兼容用空方法(以防 Designer.cs 还引用旧 handler) ===
        private void Form1_Load(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
    }
}
