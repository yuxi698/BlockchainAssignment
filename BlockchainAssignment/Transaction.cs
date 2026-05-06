using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    class Transaction
    {
        /* Transaction Variables */
        public DateTime timestamp;                                 // Time of creation
        public String senderAddress, recipientAddress;      // Participants public key addresses
        public double amount, fee;                          // Quantities transferred
        public String hash, signature;                      // Attributes for verification of validity

        /* Transaction Constructor */
        public Transaction(String from, String to, double amount, double fee, String privateKey)
        {
            this.timestamp = DateTime.Now;
            this.senderAddress = from;
            this.recipientAddress = to;
            this.amount = amount;
            this.fee = fee;
            this.hash = CreateHash();                                                  // Hash the transaction attributes
            this.signature = Wallet.Wallet.CreateSignature(from, privateKey, hash);    // Sign the hash with the senders private key ensuring validity
        }

        /* Transaction Functions */
        public String CreateHash()  // Hash the transaction attributes using SHA256
        {
            String hash = String.Empty;
            SHA256 hasher = SHA256Managed.Create();
            String input = timestamp + senderAddress + recipientAddress + amount + fee;   // Concatenate all transaction properties
            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));          // Apply the hash function to the "input" string

            foreach (byte x in hashByte)
                hash += String.Format("{0:x2}", x);                                       // Reformat to a string

            return hash;
        }

        public override string ToString()  // Represent a transaction as a string for output to UI
        {
            return " [TRANSACTION START]"
                + "\n Timestamp: " + timestamp
                + "\n -- Verification --"
                + "\n Hash: " + hash
                + "\n Signature: " + signature
                + "\n -- Quantities --"
                + "\n Transferred: " + amount + " AssignmentCoin"
                + "\t Fee: " + fee
                + "\n -- Participants --"
                + "\n Sender: " + senderAddress
                + "\n Receiver: " + recipientAddress
                + "\n [TRANSACTION END]";
        }
    }
}