using System.Linq;

namespace SetGame
{
	partial class CardDetails
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
			this.comboBoxColor = new System.Windows.Forms.ComboBox();
			this.comboBoxShape = new System.Windows.Forms.ComboBox();
			this.comboBoxFill = new System.Windows.Forms.ComboBox();
			this.comboBoxCount = new System.Windows.Forms.ComboBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBoxColor
			// 
			this.comboBoxColor.FormattingEnabled = true;
			this.comboBoxColor.Location = new System.Drawing.Point(13, 13);
			this.comboBoxColor.Name = "comboBoxColor";
			this.comboBoxColor.Size = new System.Drawing.Size(81, 21);
			this.comboBoxColor.TabIndex = 0;
			// 
			// comboBoxShape
			// 
			this.comboBoxShape.FormattingEnabled = true;
			this.comboBoxShape.Location = new System.Drawing.Point(12, 40);
			this.comboBoxShape.Name = "comboBoxShape";
			this.comboBoxShape.Size = new System.Drawing.Size(81, 21);
			this.comboBoxShape.TabIndex = 1;
			// 
			// comboBoxFill
			// 
			this.comboBoxFill.FormattingEnabled = true;
			this.comboBoxFill.Location = new System.Drawing.Point(13, 67);
			this.comboBoxFill.Name = "comboBoxFill";
			this.comboBoxFill.Size = new System.Drawing.Size(81, 21);
			this.comboBoxFill.TabIndex = 2;
			// 
			// comboBoxCount
			// 
			this.comboBoxCount.FormattingEnabled = true;
			this.comboBoxCount.Location = new System.Drawing.Point(13, 94);
			this.comboBoxCount.Name = "comboBoxCount";
			this.comboBoxCount.Size = new System.Drawing.Size(81, 21);
			this.comboBoxCount.TabIndex = 3;
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(197, 92);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 4;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.BackColor = System.Drawing.Color.Maroon;
			this.buttonDelete.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonDelete.Location = new System.Drawing.Point(197, 13);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(75, 23);
			this.buttonDelete.TabIndex = 5;
			this.buttonDelete.Text = "Delete";
			this.buttonDelete.UseVisualStyleBackColor = false;
			this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
			// 
			// CardDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 127);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.comboBoxCount);
			this.Controls.Add(this.comboBoxFill);
			this.Controls.Add(this.comboBoxShape);
			this.Controls.Add(this.comboBoxColor);
			this.Name = "CardDetails";
			this.Text = "CardDetails";
			this.ResumeLayout(false);

		}

		private void InitializeDynamic()
		{
			Types.SetDataSource(this.comboBoxColor, typeof(Color));
			Types.SetDataSource(this.comboBoxShape, typeof(Shape));
			Types.SetDataSource(this.comboBoxFill, typeof(Fill));
			Types.SetDataSource(this.comboBoxCount, typeof(Count));

			var card = this._button.Tag as Card;
			// Show card properties
			if (card == null)
			{
				this.comboBoxColor.SelectedValue = "Color";
				this.comboBoxShape.SelectedValue = "Shape";
				this.comboBoxFill.SelectedValue = "Fill";
				this.comboBoxCount.SelectedValue = "Count";
			}
			else
			{
				this.comboBoxColor.SelectedValue = card.Color;
				this.comboBoxShape.SelectedValue = card.Shape;
				this.comboBoxFill.SelectedValue = card.Fill;
				this.comboBoxCount.SelectedValue = card.Count;
			}
		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxColor;
		private System.Windows.Forms.ComboBox comboBoxShape;
		private System.Windows.Forms.ComboBox comboBoxFill;
		private System.Windows.Forms.ComboBox comboBoxCount;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonDelete;
	}
}