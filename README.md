# CS3BC Blockchain Assignment — Mini Blockchain

University of Reading · CS3BC Blockchain and Security · Coursework 2026  
Student Number: **31808439**

## Overview

A simple offline blockchain implemented in C# with a Windows Forms UI. It supports proof-of-work mining,
ECDSA-signed transactions, a transaction pool, Merkle-root validation, balance checking, and three Part 6
extensions (multi-threaded mining, adaptive difficulty, miner transaction-selection strategies).

## Build & Run

- **IDE**: Visual Studio 2022 (any edition)
- **Target**: .NET Framework 4.7.2 / Windows Forms
- Open `BlockchainAssignment.sln` → press `F5` to run.

## Implemented (per the brief)

### Parts 1–5
- **Part 1** — Project setup, custom UI controls, event handlers
- **Part 2** — `Block` and `Blockchain` classes, Genesis block, SHA-256 hashing
- **Part 3** — ECDSA P-256 wallets, signed transactions, transaction pool
- **Part 4** — Proof-of-Work (`Block.Mine`), difficulty 4, coinbase rewards + fees
- **Part 5** — Hash-chain coherence, Merkle-root validation, balance checking

### Part 6 (3 of 4 tasks)
- **Task 1 — Multi-threaded PoW** (`Block.RunBenchmark`): N-thread parallel mining with e-nonce stride 
  (thread *i* tests nonces *{i, i+N, i+2N, ...}*) and `CancellationTokenSource` for cooperative shutdown.
- **Task 2 — Adaptive Difficulty** (`Blockchain.RecomputeDifficulty`): SMA-4 of actual mining durations 
  with ±30% hysteresis band, integer ±1 adjustment, clamped to [3, 6].
- **Task 3 — Miner Transaction Selection** (`Blockchain.SelectTransactions`): four policies — Greedy 
  (highest fee), Altruistic (oldest first), Random, and Address Preference (miner-related first).

## File Layout

BlockchainAssignment/

├── Block.cs           — Block, PoW (single + parallel benchmark), Merkle root

├── Blockchain.cs      — Chain, transaction pool, validation, adaptive difficulty, transaction-selection policies

├── Transaction.cs     — Transaction model + SHA-256 hashing

├── BlockchainApp.cs   — Windows Forms event handlers

├── Wallet/Wallet.cs   — ECDSA-P256 key generation, signing, validation (provided)

└── HashCode/HashTools.cs — Hash combine helpers (provided)
