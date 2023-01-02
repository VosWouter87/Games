namespace MaskEdit
{
    partial class Mask : System.Windows.Forms.Form
    {
        private const int startX = 50, startY = 690;
        private const int size = 80;
        private static readonly object zero = (object)0;
        private static readonly object one = (object)1;
        private readonly Button[] squares = new Button[64];
        private ulong board = 0;

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
            this.decCalculated = new System.Windows.Forms.NumericUpDown();
            this.hexCalculated = new System.Windows.Forms.TextBox();
            this.indexesCalculated = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.decCalculated)).BeginInit();
            this.SuspendLayout();
            // 
            // decCalculated
            // 
            this.decCalculated.Location = new System.Drawing.Point(800, 100);
            this.decCalculated.Maximum = new decimal(new int[] {
            -1,
            -1,
            0,
            0});
            this.decCalculated.Name = "decCalculated";
            this.decCalculated.Size = new System.Drawing.Size(350, 31);
            this.decCalculated.TabIndex = 0;
            this.decCalculated.KeyUp += DecCalculated_KeyUp;
            // 
            // hexCalculated
            // 
            this.hexCalculated.Location = new System.Drawing.Point(800, 150);
            this.hexCalculated.Name = "hexCalculated";
            this.hexCalculated.Size = new System.Drawing.Size(350, 31);
            this.hexCalculated.TabIndex = 1;
            this.hexCalculated.Text = "hex";
            this.hexCalculated.KeyUp += HexCalculated_KeyUp;
            // 
            // indexesCalculated
            // 
            this.indexesCalculated.Location = new System.Drawing.Point(800, 211);
            this.indexesCalculated.Name = "indexesCalculated";
            this.indexesCalculated.Size = new System.Drawing.Size(350, 31);
            this.indexesCalculated.TabIndex = 2;
            this.indexesCalculated.Text = "";
            this.indexesCalculated.KeyUp += IndexesCalculated_KeyUp;
            // 
            // Mask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.indexesCalculated);
            this.Controls.Add(this.hexCalculated);
            this.Controls.Add(this.decCalculated);
            this.Name = "Mask";
            this.Text = "Mask generator";
            ((System.ComponentModel.ISupportInitialize)(this.decCalculated)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void InitializeDynamic()
        {
            int x = startX, y = startY;
            bool on = false;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    square = new Button();
                    square.Location = new Point(x, y);
                    square.Name = "square_" + x + "_" + y;
                    square.Size = new Size(size, size);
                    var index = i * 8 + j;
                    square.TabIndex = index;
                    squares[index] = square;
                    square.Text = "0";
                    square.BackColor = on ? Color.Beige : Color.BurlyWood;
                    //square.ForeColor = on ? Color.Black : Color.White;
                    square.UseVisualStyleBackColor = false;
                    square.Click += Square_Click;
                    square.Tag = zero;
                    this.Controls.Add(square);
                    x += size;
                    on = !on;
                }

                on = !on;
                x = startX;
                y -= size;
            }
        }

        private Button square;
        private NumericUpDown decCalculated;
        private TextBox hexCalculated;
        private TextBox indexesCalculated;
    }
}