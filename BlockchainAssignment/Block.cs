using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace BlockchainAssignment
{
    class Block
    {
        /* Block Variables */
        public DateTime timestamp;                 // Time of creation
        public int index,                          // Position of the block in the sequence of blocks
                   difficulty = 4;                 // An arbitrary number of 0's to proceed a hash value
        public String prevHash,                    // A reference pointer to the previous block
                      hash,                        // The current blocks "identity"
                      merkleRoot,                  // The merkle root of all transactions in the block
                      minerAddress;                // Public Key (Wallet Address) of the Miner
        public List<Transaction> transactionList;  // List of transactions in this block

        // Proof-of-work
        public long nonce;                         // Number used once for Proof-of-Work and mining

        // Rewards
        public double reward;                      // Simple fixed reward established by "Coinbase"
        
        // Part 6 Task 2: Track actual mining duration for adaptive difficulty
        public double mineDurationSeconds = 0;
        
        /* Block constructors */
        public Block()  // Genesis Block
        {
            this.timestamp = DateTime.Now;
            this.index = 0;
            this.transactionList = new List<Transaction>();
            this.hash = Mine();
        }

        public Block(Block lastBlock, List<Transaction> transactions, String minerAddress)  // Standard Block
        {
            this.timestamp = DateTime.Now;
            this.index = lastBlock.index + 1;
            this.prevHash = lastBlock.hash;

            this.minerAddress = minerAddress;                              // The wallet to be credited the reward for the mining effort
            this.reward = 1.0;                                             // Assign a simple fixed value reward

            transactions.Add(createRewardTransaction(transactions));       // Create and append the reward transaction
            this.transactionList = new List<Transaction>(transactions);    // Assign provided transactions to the block

            this.merkleRoot = MerkleRoot(transactionList);                 // Calculate the merkle root of the blocks transactions
            this.hash = Mine();                                            // Conduct PoW to create a hash which meets the given difficulty requirement
        }

        /* Block Functions */
        public String CreateHash()  // Hashes the entire Block object
        {
            String hash = String.Empty;
            SHA256 hasher = SHA256Managed.Create();

            String input = timestamp.ToString() + index + prevHash + nonce + merkleRoot;  // Concatenate all of the blocks properties including nonce
            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));           // Apply the SHA hash function

            foreach (byte x in hashByte)
                hash += String.Format("{0:x2}", x);                                        // Reformat to a string

            return hash;
        }

        public String Mine()  // Create a Hash which satisfies the difficulty level required for PoW
        {
            Stopwatch sw = Stopwatch.StartNew();   // Part 6 Task 2: track actual mining time
            nonce = 0;                                          // Initalise the nonce
            String hash = CreateHash();                         // Hash the block
            String re = new string('0', difficulty);            // A string representing the "difficulty" for analysing the PoW requirement

            while (!hash.StartsWith(re))                        // Check the resultant hash against the "re" string
            {
                nonce++;                                        // Increment the nonce
                hash = CreateHash();                            // Rehash with the new nonce
            }
            sw.Stop();
            mineDurationSeconds = sw.Elapsed.TotalSeconds;
            return hash;                                        // Return the hash meeting the difficulty requirement
        }

        public static String MerkleRoot(List<Transaction> transactionList)  // Merkle Root Algorithm - Encodes transactions within a block into a single hash
        {
            List<String> hashes = transactionList.Select(t => t.hash).ToList();  // Get a list of transaction hashes for "combining"

            // Handle Blocks with...
            if (hashes.Count == 0)  // No transactions
            {
                return String.Empty;
            }
            if (hashes.Count == 1)  // One transaction - hash with "self"
            {
                return HashCode.HashTools.CombineHash(hashes[0], hashes[0]);
            }
            while (hashes.Count != 1)  // Multiple transactions - Repeat until tree has been traversed
            {
                List<String> merkleLeaves = new List<String>();  // Keep track of current "level" of the tree

                for (int i = 0; i < hashes.Count; i += 2)        // Step over neighbouring pair combining each
                {
                    if (i == hashes.Count - 1)
                    {
                        merkleLeaves.Add(HashCode.HashTools.CombineHash(hashes[i], hashes[i]));      // Handle an odd number of leaves
                    }
                    else
                    {
                        merkleLeaves.Add(HashCode.HashTools.CombineHash(hashes[i], hashes[i + 1]));  // Hash neighbours leaves
                    }
                }
                hashes = merkleLeaves;  // Update the working "layer"
            }
            return hashes[0];  // Return the root node
        }

        public Transaction createRewardTransaction(List<Transaction> transactions)  // Create reward for incentivising the mining of block
        {
            double fees = transactions.Aggregate(0.0, (acc, t) => acc + t.fee);                  // Sum all transaction fees
            return new Transaction("Mine Rewards", minerAddress, (reward + fees), 0, "");        // Issue reward as a transaction in the new block
        }

        public override string ToString()  // Concatenate all properties to output to the UI
        {
            return "[BLOCK START]"
                + "\nIndex: " + index
                + "\tTimestamp: " + timestamp
                + "\nPrevious Hash: " + prevHash
                + "\n-- PoW --"
                + "\nDifficulty Level: " + difficulty
                + "\nNonce: " + nonce
                + "\nHash: " + hash
                + "\n-- Rewards --"
                + "\nReward: " + reward
                + "\nMiners Address: " + minerAddress
                + "\n-- " + transactionList.Count + " Transactions --"
                + "\nMerkle Root: " + merkleRoot
                + "\n" + String.Join("\n", transactionList)
                + "\n[BLOCK END]";
        }

        /* ===== Part 6 Task 1: Multi-threaded PoW Benchmark ===== */
        // Static benchmark method - runs PoW in parallel with e-nonce stride
        // Each thread i tests nonces {i, i+N, i+2N, ...} where N = threadCount
        // First thread to find a valid hash cancels the others
        public static (long nonce, string hash, long elapsedMs) RunBenchmark(int difficulty, int threadCount, string baseInput)
        {
            int actualThreads = Math.Max(1, threadCount);
            string target = new string('0', difficulty);

            long resultNonce = -1;
            string resultHash = null;
            CancellationTokenSource cts = new CancellationTokenSource();
            object lockObj = new object();

            Stopwatch sw = Stopwatch.StartNew();

            Task[] tasks = new Task[actualThreads];
            for (int t = 0; t < actualThreads; t++)
            {
                int threadIdx = t;  // capture for closure
                tasks[t] = Task.Run(() =>
                {
                    long localNonce = threadIdx;  // e-nonce: each thread starts at unique position
                    while (!cts.Token.IsCancellationRequested)
                    {
                        string h = ComputeSHA256(baseInput + localNonce);
                        if (h.StartsWith(target))
                        {
                            lock (lockObj)
                            {
                                if (resultHash == null)
                                {
                                    resultHash = h;
                                    resultNonce = localNonce;
                                    cts.Cancel();  // signal other threads to stop
                                }
                            }
                            return;
                        }
                        localNonce += actualThreads;  // stride avoids duplicated work
                    }
                });
            }

            try { Task.WaitAll(tasks); } catch (AggregateException) { /* expected on cancel */ }
            sw.Stop();

            return (resultNonce, resultHash, sw.ElapsedMilliseconds);
        }

        // SHA256 helper used by the benchmark
        private static string ComputeSHA256(string input)
        {
            SHA256 hasher = SHA256Managed.Create();
            byte[] bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        /* ===== Part 6 Task 2: Adaptive Difficulty Support ===== */
        // Public read-only access to private fields (needed by Blockchain to read timestamps)
        public DateTime Timestamp { get { return timestamp; } }
        public int Index { get { return index; } }
        public int Difficulty { get { return difficulty; } }

        // Constructor overload that accepts a custom difficulty (for adaptive difficulty)
        public Block(Block lastBlock, List<Transaction> transactions, String minerAddress, int customDifficulty)
        {
            this.difficulty = customDifficulty;
            this.timestamp = DateTime.Now;
            this.index = lastBlock.index + 1;
            this.prevHash = lastBlock.hash;
            this.minerAddress = minerAddress;
            this.reward = 1.0;
            transactions.Add(createRewardTransaction(transactions));
            this.transactionList = new List<Transaction>(transactions);
            this.merkleRoot = MerkleRoot(transactionList);
            this.hash = Mine();
        }
    }
}