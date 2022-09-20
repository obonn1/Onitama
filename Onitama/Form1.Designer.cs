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
            Onitama.Game gameState1 = new Onitama.Game();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Onitama.GameVisuals gameVisuals1 = new Onitama.GameVisuals(gameState1);
            this.oniBoard1 = new Onitama.OniBoard();
            this.SuspendLayout();
            // 
            // oniBoard1
            // 
            this.oniBoard1.BackColor = System.Drawing.Color.DarkGray;
            this.oniBoard1.Dock = System.Windows.Forms.DockStyle.Fill;
            gameState1.ActiveCard = null;
            gameState1.ActiveCardLocation = null;
            gameState1.ActiveSquare = null;
            gameState1.ActiveScreen = Onitama.Screens.Tutorial;
            gameState1.BlueMaster = new System.Drawing.Point(0, 2);
            gameState1.BlueStudents = ((System.Collections.Generic.List<System.Drawing.Point>)(resources.GetObject("gameState1.BlueStudents")));
            gameState1.CurrentTeam = Onitama.Team.Red;
            gameState1.IsMenuOpen = false;
            gameState1.MouseDownLocation = null;
            gameState1.PossibleMoves = ((System.Collections.Generic.List<System.Drawing.Point>)(resources.GetObject("gameState1.PossibleMoves")));
            gameState1.RedMaster = new System.Drawing.Point(4, 2);
            gameState1.RedStudents = ((System.Collections.Generic.List<System.Drawing.Point>)(resources.GetObject("gameState1.RedStudents")));
            gameState1.TutorialStep = 1;
            this.oniBoard1.Game = gameState1;
            this.oniBoard1.GridColor = System.Drawing.Color.Green;
            this.oniBoard1.Location = new System.Drawing.Point(0, 0);
            this.oniBoard1.Margin = new System.Windows.Forms.Padding(0);
            this.oniBoard1.MinimumSize = new System.Drawing.Size(262, 225);
            this.oniBoard1.Name = "oniBoard1";
            this.oniBoard1.Size = new System.Drawing.Size(1031, 658);
            this.oniBoard1.TabIndex = 0;
            this.oniBoard1.Text = "oniBoard1";
            gameVisuals1.ActiveCard = null;
            gameVisuals1.ActiveStudent = null;
            gameVisuals1.ActiveScreen = Onitama.Screens.Tutorial;
            gameVisuals1.BlueMaster = new System.Drawing.Point(0, 2);
            gameVisuals1.BlueStudents = ((System.Collections.Generic.List<System.Drawing.Point>)(resources.GetObject("gameVisuals1.BlueStudents")));
            gameVisuals1.CurrentTeam = Onitama.Team.Red;
            gameVisuals1.IsMenuOpen = false;
            gameVisuals1.PossibleMoves = ((System.Collections.Generic.List<System.Drawing.Point>)(resources.GetObject("gameVisuals1.PossibleMoves")));
            gameVisuals1.RedMaster = new System.Drawing.Point(4, 2);
            gameVisuals1.RedStudents = ((System.Collections.Generic.List<System.Drawing.Point>)(resources.GetObject("gameVisuals1.RedStudents")));
            gameVisuals1.TutorialStep = 1;
            this.oniBoard1.Visuals = gameVisuals1;
            this.oniBoard1.Click += new System.EventHandler(this.OniBoard1_Click);
            this.oniBoard1.Paint += new System.Windows.Forms.PaintEventHandler(this.OniBoard1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1031, 658);
            this.Controls.Add(this.oniBoard1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(264, 145);
            this.Name = "Form1";
            this.Text = "Onitama";
            this.ResumeLayout(false);

        }

        #endregion

        private OniBoard oniBoard1;
    }
}
