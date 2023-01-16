namespace CantStop
{
	partial class CantStop
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
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.board = new System.Windows.Forms.PictureBox();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.board)).BeginInit();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(657, 28);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // newGameToolStripMenuItem
      // 
      this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
      this.newGameToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
      this.newGameToolStripMenuItem.Text = "New Game";
      this.newGameToolStripMenuItem.Click += new System.EventHandler(this.NewGameToolStripMenuItem_Click);
      // 
      // board
      // 
      this.board.Location = new System.Drawing.Point(0, 31);
      this.board.Name = "board";
      this.board.Size = new System.Drawing.Size(624, 362);
      this.board.TabIndex = 2;
      this.board.TabStop = false;
      // 
      // CantStop
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(657, 510);
      this.Controls.Add(this.board);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.Name = "CantStop";
      this.Text = "Can\'t Stop";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.board)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private void InitializeDynamic()
		{
		}

		//private Line field;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.PictureBox board;
    }
}

