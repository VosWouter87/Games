using System.Text;

namespace MaskEdit
{
    public partial class Mask : Form
    {
        public Mask()
        {
            InitializeComponent();
            InitializeDynamic();
        }

        private void DecCalculated_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFromDec();
                e.SuppressKeyPress = true;
            }
        }

        private void HexCalculated_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFromHex();
                e.SuppressKeyPress = true;
            }
        }

        private void IndexesCalculated_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFromIndexes();
                e.SuppressKeyPress = true;
            }
        }

        private void Square_Click(object sender, EventArgs e)
        {
            var square = sender as Button;
            if (square != null && square.Focused)
            {
                SetOn(square, square.Tag != one);
                CalculateBoard();
                SetHex();
            }
        }

        private static void SetOn(Button square, bool on)
        {
            if (on)
            {
                //square.BackColor = Color.White;
                square.Text = "1";
                square.Tag = one;
                //square.ForeColor = Color.Black;
            }
            else
            {
                //square.BackColor = Color.Brown;
                square.Text = "0";
                square.Tag = zero;
                //square.ForeColor = Color.White;
            }
        }

        private void ApplyFromDec()
        {
            board = (ulong)this.decCalculated.Value;
            ShowFields();
            SetHex();
        }

        private void ApplyFromHex()
        {
            var hex = this.hexCalculated.Text;
            if (String.IsNullOrWhiteSpace(hex) || hex == "0" || hex == "0x0")
            {
                decCalculated.Value = 0;
                ClearBoard();
            }
            else
            {
                if (hex.EndsWith(", "))
                {
                    hex = hex.Substring(0, hex.Length - 2);
                }
                board = Convert.ToUInt64(hex, 16);
                decCalculated.Value = board;
                ShowFields();
            }
        }

        private void ApplyFromIndexes()
        {
            board = 0;
            foreach (var number in indexesCalculated.Text.Split(' '))
            {
                if (int.TryParse(number, out int index))
                {
                    board |= (1UL << index);
                }
            }
            UpdateFromBoard();
        }

        private void ShowFields()
        {
            ulong mask = 1;
            for (var i = 0; i < squares.Length; i++)
            {
                SetOn(squares[i], (board & mask) > 0);
                mask <<= 1;
            }
        }

        private void ClearBoard()
        {
            board = 0;
            for (var i = 0; i < squares.Length; i++)
            {
                SetOn(squares[i], false);
            }
        }

        private void CalculateBoard()
        {
            ulong mask = 1;
            board = 0;
            var indexes = new StringBuilder();
            for (var i = 0; i < squares.Length; i++)
            {
                if (squares[i].Tag == one)
                {
                    board += mask;
                    indexes.Append(i.ToString() + " ");
                }
                mask <<= 1;
            }
            indexesCalculated.Text = indexes.ToString();
            UpdateFromBoard();
        }

        private void UpdateFromBoard()
        {
            decCalculated.Value = board;
            ShowFields();
            SetHex();
        }

        private void SetHex()
        {
            hexCalculated.Text = string.Format("0x{0:X}", board);
        }
    }
}