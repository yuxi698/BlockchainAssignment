namespace BlockchainAssignment
{
    partial class BlockchainApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.output = new System.Windows.Forms.RichTextBox();
            this.ReadAll = new System.Windows.Forms.Button();
            this.blockNo = new System.Windows.Forms.TextBox();
            this.publicKey = new System.Windows.Forms.TextBox();
            this.privateKey = new System.Windows.Forms.TextBox();
            this.receiver = new System.Windows.Forms.TextBox();
            this.amount = new System.Windows.Forms.TextBox();
            this.fee = new System.Windows.Forms.TextBox();
            this.PrintBlock = new System.Windows.Forms.Button();
            this.PrintPendingTransactions = new System.Windows.Forms.Button();
            this.GenerateWallet = new System.Windows.Forms.Button();
            this.ValidateKeys = new System.Windows.Forms.Button();
            this.CheckBalance = new System.Windows.Forms.Button();
            this.CreateTransaction = new System.Windows.Forms.Button();
            this.NewBlock = new System.Windows.Forms.Button();
            this.Validate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Benchmark = new System.Windows.Forms.Button();
            this.MineGreedy = new System.Windows.Forms.Button();
            this.Compare = new System.Windows.Forms.Button();
            this.MineAltruistic = new System.Windows.Forms.Button();
            this.MineRandom = new System.Windows.Forms.Button();
            this.MineAddrPref = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.SystemColors.InfoText;
            this.output.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.output.Location = new System.Drawing.Point(13, 0);
            this.output.Margin = new System.Windows.Forms.Padding(4);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(984, 433);
            this.output.TabIndex = 0;
            this.output.Text = "";
            this.output.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // ReadAll
            // 
            this.ReadAll.Location = new System.Drawing.Point(536, 441);
            this.ReadAll.Name = "ReadAll";
            this.ReadAll.Size = new System.Drawing.Size(117, 37);
            this.ReadAll.TabIndex = 1;
            this.ReadAll.Text = "Read All";
            this.ReadAll.UseVisualStyleBackColor = true;
            this.ReadAll.Click += new System.EventHandler(this.ReadAll_Click);
            // 
            // blockNo
            // 
            this.blockNo.Location = new System.Drawing.Point(278, 446);
            this.blockNo.Name = "blockNo";
            this.blockNo.Size = new System.Drawing.Size(215, 28);
            this.blockNo.TabIndex = 2;
            // 
            // publicKey
            // 
            this.publicKey.Location = new System.Drawing.Point(329, 490);
            this.publicKey.Name = "publicKey";
            this.publicKey.Size = new System.Drawing.Size(324, 28);
            this.publicKey.TabIndex = 3;
            // 
            // privateKey
            // 
            this.privateKey.Location = new System.Drawing.Point(329, 531);
            this.privateKey.Name = "privateKey";
            this.privateKey.Size = new System.Drawing.Size(324, 28);
            this.privateKey.TabIndex = 4;
            // 
            // receiver
            // 
            this.receiver.Location = new System.Drawing.Point(782, 572);
            this.receiver.Name = "receiver";
            this.receiver.Size = new System.Drawing.Size(179, 28);
            this.receiver.TabIndex = 5;
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(293, 572);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(131, 28);
            this.amount.TabIndex = 6;
            // 
            // fee
            // 
            this.fee.Location = new System.Drawing.Point(499, 572);
            this.fee.Name = "fee";
            this.fee.Size = new System.Drawing.Size(154, 28);
            this.fee.TabIndex = 7;
            // 
            // PrintBlock
            // 
            this.PrintBlock.Location = new System.Drawing.Point(38, 440);
            this.PrintBlock.Name = "PrintBlock";
            this.PrintBlock.Size = new System.Drawing.Size(129, 37);
            this.PrintBlock.TabIndex = 8;
            this.PrintBlock.Text = "Print Block";
            this.PrintBlock.UseVisualStyleBackColor = true;
            this.PrintBlock.Click += new System.EventHandler(this.PrintBlock_Click);
            // 
            // PrintPendingTransactions
            // 
            this.PrintPendingTransactions.Location = new System.Drawing.Point(697, 441);
            this.PrintPendingTransactions.Name = "PrintPendingTransactions";
            this.PrintPendingTransactions.Size = new System.Drawing.Size(264, 36);
            this.PrintPendingTransactions.TabIndex = 9;
            this.PrintPendingTransactions.Text = "Read Pending Transactions";
            this.PrintPendingTransactions.UseVisualStyleBackColor = true;
            this.PrintPendingTransactions.Click += new System.EventHandler(this.PrintPendingTransactions_Click);
            // 
            // GenerateWallet
            // 
            this.GenerateWallet.Location = new System.Drawing.Point(38, 483);
            this.GenerateWallet.Name = "GenerateWallet";
            this.GenerateWallet.Size = new System.Drawing.Size(158, 38);
            this.GenerateWallet.TabIndex = 10;
            this.GenerateWallet.Text = "Generate Wallet";
            this.GenerateWallet.UseVisualStyleBackColor = true;
            this.GenerateWallet.Click += new System.EventHandler(this.GenerateWallet_Click);
            // 
            // ValidateKeys
            // 
            this.ValidateKeys.Location = new System.Drawing.Point(38, 527);
            this.ValidateKeys.Name = "ValidateKeys";
            this.ValidateKeys.Size = new System.Drawing.Size(150, 33);
            this.ValidateKeys.TabIndex = 11;
            this.ValidateKeys.Text = " Validate Keys";
            this.ValidateKeys.UseVisualStyleBackColor = true;
            this.ValidateKeys.Click += new System.EventHandler(this.ValidateKeys_Click);
            // 
            // CheckBalance
            // 
            this.CheckBalance.Location = new System.Drawing.Point(38, 566);
            this.CheckBalance.Name = "CheckBalance";
            this.CheckBalance.Size = new System.Drawing.Size(138, 37);
            this.CheckBalance.TabIndex = 12;
            this.CheckBalance.Text = "Check Balance";
            this.CheckBalance.UseVisualStyleBackColor = true;
            this.CheckBalance.Click += new System.EventHandler(this.CheckBalance_Click);
            // 
            // CreateTransaction
            // 
            this.CreateTransaction.Location = new System.Drawing.Point(697, 516);
            this.CreateTransaction.Name = "CreateTransaction";
            this.CreateTransaction.Size = new System.Drawing.Size(264, 36);
            this.CreateTransaction.TabIndex = 13;
            this.CreateTransaction.Text = "Create Transaction";
            this.CreateTransaction.UseVisualStyleBackColor = true;
            this.CreateTransaction.Click += new System.EventHandler(this.CreateTransaction_Click);
            // 
            // NewBlock
            // 
            this.NewBlock.Location = new System.Drawing.Point(38, 616);
            this.NewBlock.Name = "NewBlock";
            this.NewBlock.Size = new System.Drawing.Size(195, 38);
            this.NewBlock.TabIndex = 14;
            this.NewBlock.Text = "Generate New Block";
            this.NewBlock.UseVisualStyleBackColor = true;
            this.NewBlock.Click += new System.EventHandler(this.NewBlock_Click);
            // 
            // Validate
            // 
            this.Validate.Location = new System.Drawing.Point(278, 616);
            this.Validate.Name = "Validate";
            this.Validate.Size = new System.Drawing.Size(271, 38);
            this.Validate.TabIndex = 15;
            this.Validate.Text = "Full Blockchain Validation";
            this.Validate.UseVisualStyleBackColor = true;
            this.Validate.Click += new System.EventHandler(this.Validate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 493);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "Public Key:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 534);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Private Key:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 575);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "Amount:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(440, 578);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 19;
            this.label4.Text = "Fee:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(687, 578);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 18);
            this.label5.TabIndex = 20;
            this.label5.Text = "Receiver:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(198, 450);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 21;
            this.label6.Text = "Block #:";
            // 
            // Benchmark
            // 
            this.Benchmark.Location = new System.Drawing.Point(690, 616);
            this.Benchmark.Name = "Benchmark";
            this.Benchmark.Size = new System.Drawing.Size(208, 38);
            this.Benchmark.TabIndex = 22;
            this.Benchmark.Text = "Mining Benchmark";
            this.Benchmark.UseVisualStyleBackColor = true;
            this.Benchmark.Click += new System.EventHandler(this.Benchmark_Click);
            // 
            // MineGreedy
            // 
            this.MineGreedy.Location = new System.Drawing.Point(38, 661);
            this.MineGreedy.Name = "MineGreedy";
            this.MineGreedy.Size = new System.Drawing.Size(178, 32);
            this.MineGreedy.TabIndex = 23;
            this.MineGreedy.Text = "Mine (Greedy)";
            this.MineGreedy.UseVisualStyleBackColor = true;
            this.MineGreedy.Click += new System.EventHandler(this.MineGreedy_Click);
            // 
            // Compare
            // 
            this.Compare.Location = new System.Drawing.Point(786, 661);
            this.Compare.Name = "Compare";
            this.Compare.Size = new System.Drawing.Size(211, 32);
            this.Compare.TabIndex = 24;
            this.Compare.Text = "Compare Strategies";
            this.Compare.UseVisualStyleBackColor = true;
            this.Compare.Click += new System.EventHandler(this.Compare_Click);
            // 
            // MineAltruistic
            // 
            this.MineAltruistic.Location = new System.Drawing.Point(222, 661);
            this.MineAltruistic.Name = "MineAltruistic";
            this.MineAltruistic.Size = new System.Drawing.Size(210, 32);
            this.MineAltruistic.TabIndex = 25;
            this.MineAltruistic.Text = "Mine (Altruistic)";
            this.MineAltruistic.UseVisualStyleBackColor = true;
            this.MineAltruistic.Click += new System.EventHandler(this.MineAltruistic_Click);
            // 
            // MineRandom
            // 
            this.MineRandom.Location = new System.Drawing.Point(438, 660);
            this.MineRandom.Name = "MineRandom";
            this.MineRandom.Size = new System.Drawing.Size(168, 32);
            this.MineRandom.TabIndex = 26;
            this.MineRandom.Text = "Mine (Random)";
            this.MineRandom.UseVisualStyleBackColor = true;
            this.MineRandom.Click += new System.EventHandler(this.MineRandom_Click);
            // 
            // MineAddrPref
            // 
            this.MineAddrPref.Location = new System.Drawing.Point(612, 661);
            this.MineAddrPref.Name = "MineAddrPref";
            this.MineAddrPref.Size = new System.Drawing.Size(168, 32);
            this.MineAddrPref.TabIndex = 27;
            this.MineAddrPref.Text = "MineAddrPref";
            this.MineAddrPref.UseVisualStyleBackColor = true;
            this.MineAddrPref.Click += new System.EventHandler(this.MineAddrPref_Click);
            // 
            // BlockchainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1022, 716);
            this.Controls.Add(this.MineAddrPref);
            this.Controls.Add(this.MineRandom);
            this.Controls.Add(this.MineAltruistic);
            this.Controls.Add(this.Compare);
            this.Controls.Add(this.MineGreedy);
            this.Controls.Add(this.Benchmark);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Validate);
            this.Controls.Add(this.NewBlock);
            this.Controls.Add(this.CreateTransaction);
            this.Controls.Add(this.CheckBalance);
            this.Controls.Add(this.ValidateKeys);
            this.Controls.Add(this.GenerateWallet);
            this.Controls.Add(this.PrintPendingTransactions);
            this.Controls.Add(this.PrintBlock);
            this.Controls.Add(this.fee);
            this.Controls.Add(this.amount);
            this.Controls.Add(this.receiver);
            this.Controls.Add(this.privateKey);
            this.Controls.Add(this.publicKey);
            this.Controls.Add(this.blockNo);
            this.Controls.Add(this.ReadAll);
            this.Controls.Add(this.output);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BlockchainApp";
            this.Text = "Blockchain App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.Button ReadAll;
        private System.Windows.Forms.TextBox blockNo;
        private System.Windows.Forms.TextBox publicKey;
        private System.Windows.Forms.TextBox privateKey;
        private System.Windows.Forms.TextBox receiver;
        private System.Windows.Forms.TextBox amount;
        private System.Windows.Forms.TextBox fee;
        private System.Windows.Forms.Button PrintBlock;
        private System.Windows.Forms.Button PrintPendingTransactions;
        private System.Windows.Forms.Button GenerateWallet;
        private System.Windows.Forms.Button ValidateKeys;
        private System.Windows.Forms.Button CheckBalance;
        private System.Windows.Forms.Button CreateTransaction;
        private System.Windows.Forms.Button NewBlock;
        private System.Windows.Forms.Button Validate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Benchmark;
        private System.Windows.Forms.Button MineGreedy;
        private System.Windows.Forms.Button Compare;
        private System.Windows.Forms.Button MineAltruistic;
        private System.Windows.Forms.Button MineRandom;
        private System.Windows.Forms.Button MineAddrPref;
    }
}

