namespace MemoryGame
{
    partial class FormMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FirstPlayerName = new System.Windows.Forms.TextBox();
            this.SecondPlayerName = new System.Windows.Forms.TextBox();
            this.GameModeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.BoardSizeButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(24, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Player Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(24, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Second Player Name:";
            // 
            // FirstPlayerName
            // 
            this.FirstPlayerName.AccessibleName = "FirstPlayerName";
            this.FirstPlayerName.Location = new System.Drawing.Point(191, 20);
            this.FirstPlayerName.Name = "FirstPlayerName";
            this.FirstPlayerName.Size = new System.Drawing.Size(100, 22);
            this.FirstPlayerName.TabIndex = 2;
            this.FirstPlayerName.TextChanged += new System.EventHandler(this.FirstPlayerName_TextChanged);
            // 
            // SecondPlayerName
            // 
            this.SecondPlayerName.AccessibleName = "SecondPlayerName";
            this.SecondPlayerName.Enabled = false;
            this.SecondPlayerName.Location = new System.Drawing.Point(191, 49);
            this.SecondPlayerName.Name = "SecondPlayerName";
            this.SecondPlayerName.Size = new System.Drawing.Size(100, 22);
            this.SecondPlayerName.TabIndex = 3;
            this.SecondPlayerName.Text = "- computer -";
            this.SecondPlayerName.TextChanged += new System.EventHandler(this.SecondPlayerName_TextChanged);
            // 
            // GameModeButton
            // 
            this.GameModeButton.AccessibleName = "GameModeButton";
            this.GameModeButton.Location = new System.Drawing.Point(297, 47);
            this.GameModeButton.Name = "GameModeButton";
            this.GameModeButton.Size = new System.Drawing.Size(137, 24);
            this.GameModeButton.TabIndex = 4;
            this.GameModeButton.Text = "Against A Friend";
            this.GameModeButton.UseVisualStyleBackColor = true;
            this.GameModeButton.Click += new System.EventHandler(this.gameMode_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Board Size:";
            // 
            // BoardSizeButton
            // 
            this.BoardSizeButton.AccessibleName = "BoardSizeButton";
            this.BoardSizeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.BoardSizeButton.Location = new System.Drawing.Point(30, 111);
            this.BoardSizeButton.Name = "BoardSizeButton";
            this.BoardSizeButton.Size = new System.Drawing.Size(142, 99);
            this.BoardSizeButton.TabIndex = 6;
            this.BoardSizeButton.Text = "4 X 4";
            this.BoardSizeButton.UseVisualStyleBackColor = false;
            this.BoardSizeButton.Click += new System.EventHandler(this.BoardSize_Click);
            // 
            // StartButton
            // 
            this.StartButton.AccessibleName = "StartButton";
            this.StartButton.BackColor = System.Drawing.Color.LimeGreen;
            this.StartButton.Location = new System.Drawing.Point(344, 187);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 7;
            this.StartButton.Text = "Start!";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.Start_Click);
            // 
            // FormMenu
            // 
            this.AcceptButton = this.StartButton;
            this.AccessibleName = "BoardSize";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 229);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.BoardSizeButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GameModeButton);
            this.Controls.Add(this.SecondPlayerName);
            this.Controls.Add(this.FirstPlayerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FirstPlayerName;
        private System.Windows.Forms.TextBox SecondPlayerName;
        private System.Windows.Forms.Button GameModeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BoardSizeButton;
        private System.Windows.Forms.Button StartButton;
    }
}