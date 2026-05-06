using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    // Part 6 Task 3: Mining preferences
    public enum MinePreference
    {
        Greedy,            // Pick highest-fee transactions (profit-maximising)
        Altruistic,        // Pick oldest transactions first (anti-starvation)
        Random,            // Pick randomly (censorship-resistance)
        AddressPreference  // Prioritise transactions involving the miner
    }

    class Blockchain
    {
        /* Blockchain Attributes */
        public List<Block> blocks;                                            // List of block objects forming the blockchain
        private int transactionsPerBlock = 5;                                 // Maximum number of transactions per block
        public List<Transaction> transactionPool = new List<Transaction>();   // List of pending transactions to be mined

        // Single shared RNG used by the Random selection strategy.
        // Avoids the seed-collision problem caused by `new Random()` per call
        // when the system clock has not advanced between successive invocations.
        private static readonly Random rng = new Random();
        
        /* Blockchain Constructor */
        public Blockchain()  // Initialises the list of blocks and generates the genesis block
        {
            this.blocks = new List<Block>()
            {
                new Block()  // Create and append the Genesis Block
            };
        }

        /* Blockchain Functions */
        public String GetBlockAsString(int index)  // Prints the block at the specified index to the UI
        {
            if (index >= 0 && index < blocks.Count)  // Check if referenced block exists
                return blocks[index].ToString();     // Return block as a string
            else
                return "No such block exists";
        }

        public Block GetLastBlock()  // Retrieves the most recently appended block in the blockchain
        {
            return blocks[blocks.Count - 1];
        }

        public List<Transaction> GetPendingTransactions()  // Retrieve pending transactions and remove from pool
        {
            int n = Math.Min(transactionsPerBlock, transactionPool.Count);  // Determine the number of transactions to retrieve
            List<Transaction> transactions = transactionPool.GetRange(0, n);  // "Pull" transactions from the transaction list
            transactionPool.RemoveRange(0, n);
            return transactions;  // Return the extracted transactions
        }

        public static bool ValidateHash(Block block)  // Check validity of a blocks hash
        {
            String rehash = block.CreateHash();
            return rehash.Equals(block.hash);
        }

        public static bool ValidateMerkleRoot(Block block)  // Check validity of the merkle root
        {
            String reMerkle = Block.MerkleRoot(block.transactionList);
            return reMerkle.Equals(block.merkleRoot);
        }

        public double GetBalance(String address)  // Check the balance associated with a wallet
        {
            double balance = 0;  // Accumulator value for current Wallet

            foreach (Block block in blocks)  // Loop through all approved transactions
            {
                foreach (Transaction transaction in block.transactionList)
                {
                    if (transaction.recipientAddress.Equals(address))
                    {
                        balance += transaction.amount;  // Credit funds received
                    }
                    if (transaction.senderAddress.Equals(address))
                    {
                        balance -= (transaction.amount + transaction.fee);  // Debit payments placed
                    }
                }
            }
            return balance;
        }

        public override string ToString()  // Output all blocks of the blockchain as a string
        {
            return String.Join("\n", blocks);
        }

        /* ===== Part 6 Task 2: Adaptive Difficulty ===== */
        public int currentDifficulty = 4;            // Current difficulty (changes over time)
        public double targetBlockTimeSeconds = 1.0;  // Target time per block

        // Recompute difficulty based on SMA-4 (average of last 4 block intervals)
        // Returns a status string for display
        // Recompute difficulty based on SMA-4 of actual mining durations (Part 6 Task 2)
        public string RecomputeDifficulty()
        {
            if (blocks.Count < 4)
                return "[Adaptive] Need >=4 blocks to adjust (have " + blocks.Count + ")";

            // SMA of last 4 blocks' actual mining time
            double sumSeconds = 0;
            int n = 4;
            for (int i = blocks.Count - n; i < blocks.Count; i++)
            {
                sumSeconds += blocks[i].mineDurationSeconds;
            }
            double avgSeconds = sumSeconds / n;

            int oldDifficulty = currentDifficulty;
            string action;

            if (avgSeconds < targetBlockTimeSeconds * 0.7)
            {
                currentDifficulty = Math.Min(currentDifficulty + 1, 6);
                action = "TOO FAST -> difficulty UP";
            }
            else if (avgSeconds > targetBlockTimeSeconds * 1.5)
            {
                currentDifficulty = Math.Max(currentDifficulty - 1, 3);
                action = "TOO SLOW -> difficulty DOWN";
            }
            else
            {
                action = "WITHIN BAND -> no change";
            }

            return string.Format("[Adaptive] avg mine time(last 4) = {0:F3}s | target = {1:F2}s | {2} | difficulty: {3} -> {4}",
                avgSeconds, targetBlockTimeSeconds, action, oldDifficulty, currentDifficulty);
        }

        /* ===== Part 6 Task 3: Transaction Selection Strategies ===== */
        // Select transactions according to a given strategy.
        // If removeFromPool=false, this is preview-only (does not modify the pool).
        public List<Transaction> SelectTransactions(MinePreference pref, string minerAddress, bool removeFromPool = true)
        {
            int n = Math.Min(transactionsPerBlock, transactionPool.Count);
            if (n == 0) return new List<Transaction>();

            List<Transaction> selected;

            switch (pref)
            {
                case MinePreference.Greedy:
                    // Highest fees first
                    selected = transactionPool.OrderByDescending(t => t.fee).Take(n).ToList();
                    break;

                case MinePreference.Altruistic:
                    // Oldest timestamps first (longest waiting)
                    selected = transactionPool.OrderBy(t => t.timestamp).Take(n).ToList();
                    break;

                case MinePreference.Random:
                    // Fisher-Yates shuffle on a working copy, then take the first n.
                    // Uses the class-level rng so we don't re-seed on every call.
                    selected = transactionPool.ToList();
                    for (int i = selected.Count - 1; i > 0; i--)
                    {
                        int j = rng.Next(i + 1);
                        var tmp = selected[i]; selected[i] = selected[j]; selected[j] = tmp;
                    }
                    selected = selected.Take(n).ToList();
                    break;

                case MinePreference.AddressPreference:
                    // Miner's own transactions first (sender or receiver match), then by fee descending
                    selected = transactionPool
                        .OrderByDescending(t => (t.senderAddress == minerAddress || t.recipientAddress == minerAddress) ? 1 : 0)
                        .ThenByDescending(t => t.fee)
                        .Take(n)
                        .ToList();
                    break;

                default:
                    selected = transactionPool.Take(n).ToList();
                    break;
            }

            if (removeFromPool)
            {
                foreach (var t in selected) transactionPool.Remove(t);
            }
            return selected;
        }

        // Preview all 4 strategies side-by-side without modifying the pool
        public string CompareAllStrategies(string minerAddress)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== TRANSACTION SELECTION STRATEGY COMPARISON ===");
            sb.AppendLine("Pool size: " + transactionPool.Count + " | Block size: " + transactionsPerBlock);
            string mAddr = minerAddress ?? "";
            sb.AppendLine("Miner: " + (mAddr.Length > 20 ? mAddr.Substring(0, 20) + "..." : mAddr));
            sb.AppendLine();

            foreach (MinePreference pref in Enum.GetValues(typeof(MinePreference)))
            {
                var selected = SelectTransactions(pref, minerAddress, removeFromPool: false);
                sb.AppendLine("--- " + pref + " ---");
                int idx = 1;
                foreach (var t in selected)
                {
                    bool mine = (t.senderAddress == minerAddress || t.recipientAddress == minerAddress);
                    string sndShort = t.senderAddress.Length > 10 ? t.senderAddress.Substring(0, 10) : t.senderAddress;
                    string rcvShort = t.recipientAddress.Length > 10 ? t.recipientAddress.Substring(0, 10) : t.recipientAddress;
                    sb.AppendLine(string.Format("  [{0}] fee={1,-6:F2} time={2:HH:mm:ss} amt={3,-5} {4}->{5}{6}",
                        idx++, t.fee, t.timestamp, t.amount, sndShort, rcvShort, mine ? " (MINE)" : ""));
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }

}
