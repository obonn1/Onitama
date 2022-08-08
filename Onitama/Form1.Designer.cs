namespace Onitama
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Onitama.GameState gameState1 = new Onitama.GameState();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            GameVisuals gameVisuals1 = new GameVisuals();
            this.oniBoard1 = new Onitama.OniBoard();
            this.SuspendLayout();
            // 
            // oniBoard1
            // 
            this.oniBoard1.Dock = System.Windows.Forms.DockStyle.Fill;
            gameState1.ActiveCard = null;
            gameState1.CurrentTeam = Onitama.Team.Red;
            gameState1.IsGameOver = false;
            this.oniBoard1.GameState = gameState1;
            this.oniBoard1.GridColor = System.Drawing.Color.Green;
            this.oniBoard1.Location = new System.Drawing.Point(0, 0);
            this.oniBoard1.Margin = new System.Windows.Forms.Padding(0);
            this.oniBoard1.MatColor = System.Drawing.Color.Orange;
            this.oniBoard1.MinimumSize = new System.Drawing.Size(300, 300);
            this.oniBoard1.Name = "oniBoard1";
            this.oniBoard1.Size = new System.Drawing.Size(700, 700);
            this.oniBoard1.TabIndex = 0;
            this.oniBoard1.Text = "oniBoard1";
            this.oniBoard1.Visuals = gameVisuals1;
            this.oniBoard1.Click += new System.EventHandler(this.oniBoard1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.oniBoard1);
            this.MinimumSize = new System.Drawing.Size(300, 180);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OniBoard oniBoard1;
    }
}