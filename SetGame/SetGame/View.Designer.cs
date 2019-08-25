using Newtonsoft.Json;

namespace SetGame
{
	partial class View
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
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabBoard = new System.Windows.Forms.TabPage();
			this.tabEdit = new System.Windows.Forms.TabPage();
			this.richTextBoxCards = new System.Windows.Forms.RichTextBox();
			this.menuStrip1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.gameToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(490, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
			// 
			// gameToolStripMenuItem
			// 
			this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.addToolStripMenuItem});
			this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
			this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.gameToolStripMenuItem.Text = "Game";
			// 
			// showToolStripMenuItem
			// 
			this.showToolStripMenuItem.Name = "showToolStripMenuItem";
			this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.showToolStripMenuItem.Text = "Show";
			this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.addToolStripMenuItem.Text = "Add";
			this.addToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabBoard);
			this.tabControl.Controls.Add(this.tabEdit);
			this.tabControl.Location = new System.Drawing.Point(12, 27);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(466, 288);
			this.tabControl.TabIndex = 4;
			// 
			// tabBoard
			// 
			this.tabBoard.Location = new System.Drawing.Point(4, 22);
			this.tabBoard.Name = "tabBoard";
			this.tabBoard.Padding = new System.Windows.Forms.Padding(3);
			this.tabBoard.Size = new System.Drawing.Size(458, 262);
			this.tabBoard.TabIndex = 0;
			this.tabBoard.Text = "Board";
			this.tabBoard.UseVisualStyleBackColor = true;
			// 
			// tabEdit
			// 
			this.tabEdit.Controls.Add(this.richTextBoxCards);
			this.tabEdit.Location = new System.Drawing.Point(4, 22);
			this.tabEdit.Name = "tabEdit";
			this.tabEdit.Padding = new System.Windows.Forms.Padding(3);
			this.tabEdit.Size = new System.Drawing.Size(458, 262);
			this.tabEdit.TabIndex = 1;
			this.tabEdit.Text = "Edit";
			this.tabEdit.UseVisualStyleBackColor = true;
			this.tabEdit.GotFocus += TabEdit_GotFocus;
			// 
			// richTextBoxCards
			// 
			this.richTextBoxCards.Location = new System.Drawing.Point(6, 6);
			this.richTextBoxCards.Name = "richTextBoxCards";
			this.richTextBoxCards.Size = new System.Drawing.Size(446, 250);
			this.richTextBoxCards.TabIndex = 0;
			this.richTextBoxCards.Text = "";
			// 
			// View
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(490, 327);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "View";
			this.Text = "Set";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabEdit.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabBoard;
		private System.Windows.Forms.TabPage tabEdit;
		private System.Windows.Forms.RichTextBox richTextBoxCards;
	}
}

