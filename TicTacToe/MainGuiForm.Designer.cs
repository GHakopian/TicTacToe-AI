namespace TicTacToeGame
{
    partial class TicTacToeMain
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
            this.startBtn = new System.Windows.Forms.Button();
            this.logBox2 = new System.Windows.Forms.TextBox();
            this.logBox1 = new System.Windows.Forms.TextBox();
            this.humanGameBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.aiDelayBox = new System.Windows.Forms.NumericUpDown();
            this.gameBoard = new TicTacToeGame.Board();
            this.boardSizeBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.aiDelayBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardSizeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(485, 518);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(112, 23);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "Start AI vs AI Game";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.AiGameBtnClick);
            // 
            // logBox2
            // 
            this.logBox2.Location = new System.Drawing.Point(737, 12);
            this.logBox2.Multiline = true;
            this.logBox2.Name = "logBox2";
            this.logBox2.ReadOnly = true;
            this.logBox2.Size = new System.Drawing.Size(223, 529);
            this.logBox2.TabIndex = 2;
            // 
            // logBox1
            // 
            this.logBox1.Location = new System.Drawing.Point(2, 12);
            this.logBox1.Multiline = true;
            this.logBox1.Name = "logBox1";
            this.logBox1.ReadOnly = true;
            this.logBox1.Size = new System.Drawing.Size(223, 529);
            this.logBox1.TabIndex = 3;
            // 
            // humanGameBtn
            // 
            this.humanGameBtn.Location = new System.Drawing.Point(603, 518);
            this.humanGameBtn.Name = "humanGameBtn";
            this.humanGameBtn.Size = new System.Drawing.Size(128, 23);
            this.humanGameBtn.TabIndex = 4;
            this.humanGameBtn.Text = "Start human vs AI";
            this.humanGameBtn.UseVisualStyleBackColor = true;
            this.humanGameBtn.Click += new System.EventHandler(this.humanGameBtnClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(362, 523);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "AI delay";
            // 
            // aiDelayBox
            // 
            this.aiDelayBox.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.aiDelayBox.Location = new System.Drawing.Point(413, 521);
            this.aiDelayBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.aiDelayBox.Name = "aiDelayBox";
            this.aiDelayBox.Size = new System.Drawing.Size(66, 20);
            this.aiDelayBox.TabIndex = 6;
            this.aiDelayBox.ValueChanged += new System.EventHandler(this.onAiDelayChanged);
            // 
            // gameBoard
            // 
            this.gameBoard.Location = new System.Drawing.Point(231, 12);
            this.gameBoard.Name = "gameBoard";
            this.gameBoard.Size = new System.Drawing.Size(500, 500);
            this.gameBoard.TabIndex = 0;
            // 
            // boardSizeBox
            // 
            this.boardSizeBox.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.boardSizeBox.Location = new System.Drawing.Point(304, 521);
            this.boardSizeBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.boardSizeBox.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.boardSizeBox.Name = "boardSizeBox";
            this.boardSizeBox.Size = new System.Drawing.Size(52, 20);
            this.boardSizeBox.TabIndex = 8;
            this.boardSizeBox.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.boardSizeBox.ValueChanged += new System.EventHandler(this.onBoardSizeValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 523);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Board Size";
            // 
            // TicTacToeMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 548);
            this.Controls.Add(this.boardSizeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.aiDelayBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.humanGameBtn);
            this.Controls.Add(this.logBox1);
            this.Controls.Add(this.logBox2);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.gameBoard);
            this.Name = "TicTacToeMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.aiDelayBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardSizeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Board gameBoard;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.TextBox logBox2;
        private System.Windows.Forms.TextBox logBox1;
        private System.Windows.Forms.Button humanGameBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown aiDelayBox;
        private System.Windows.Forms.NumericUpDown boardSizeBox;
        private System.Windows.Forms.Label label2;
    }
}

