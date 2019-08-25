namespace CantStop
{
	partial class NewGameDialog
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
			this.radio2Players = new System.Windows.Forms.RadioButton();
			this.radio3Players = new System.Windows.Forms.RadioButton();
			this.radio4Players = new System.Windows.Forms.RadioButton();
			this.checkAllowStacking = new System.Windows.Forms.CheckBox();
			this.buttonPlay = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// radio2Players
			// 
			this.radio2Players.AutoSize = true;
			this.radio2Players.Location = new System.Drawing.Point(13, 13);
			this.radio2Players.Name = "radio2Players";
			this.radio2Players.Size = new System.Drawing.Size(67, 17);
			this.radio2Players.TabIndex = 0;
			this.radio2Players.Text = "2 players";
			this.radio2Players.UseVisualStyleBackColor = true;
			// 
			// radio3Players
			// 
			this.radio3Players.AutoSize = true;
			this.radio3Players.Location = new System.Drawing.Point(12, 36);
			this.radio3Players.Name = "radio3Players";
			this.radio3Players.Size = new System.Drawing.Size(67, 17);
			this.radio3Players.TabIndex = 1;
			this.radio3Players.Text = "3 players";
			this.radio3Players.UseVisualStyleBackColor = true;
			// 
			// radio4Players
			// 
			this.radio4Players.AutoSize = true;
			this.radio4Players.Checked = true;
			this.radio4Players.Location = new System.Drawing.Point(12, 59);
			this.radio4Players.Name = "radio4Players";
			this.radio4Players.Size = new System.Drawing.Size(67, 17);
			this.radio4Players.TabIndex = 2;
			this.radio4Players.TabStop = true;
			this.radio4Players.Text = "4 players";
			this.radio4Players.UseVisualStyleBackColor = true;
			// 
			// checkAllowStacking
			// 
			this.checkAllowStacking.AutoSize = true;
			this.checkAllowStacking.Checked = true;
			this.checkAllowStacking.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAllowStacking.Location = new System.Drawing.Point(152, 13);
			this.checkAllowStacking.Name = "checkAllowStacking";
			this.checkAllowStacking.Size = new System.Drawing.Size(96, 17);
			this.checkAllowStacking.TabIndex = 3;
			this.checkAllowStacking.Text = "Allow Stacking";
			this.checkAllowStacking.UseVisualStyleBackColor = true;
			// 
			// buttonPlay
			// 
			this.buttonPlay.Location = new System.Drawing.Point(116, 89);
			this.buttonPlay.Name = "buttonPlay";
			this.buttonPlay.Size = new System.Drawing.Size(75, 23);
			this.buttonPlay.TabIndex = 4;
			this.buttonPlay.Text = "Play";
			this.buttonPlay.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(197, 89);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// NewGameDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 124);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonPlay);
			this.Controls.Add(this.checkAllowStacking);
			this.Controls.Add(this.radio4Players);
			this.Controls.Add(this.radio3Players);
			this.Controls.Add(this.radio2Players);
			this.Name = "NewGameDialog";
			this.Text = "NewGameDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.RadioButton radio2Players;
		public System.Windows.Forms.RadioButton radio3Players;
		public System.Windows.Forms.RadioButton radio4Players;
		public System.Windows.Forms.CheckBox checkAllowStacking;
		private System.Windows.Forms.Button buttonPlay;
		private System.Windows.Forms.Button buttonCancel;
	}
}