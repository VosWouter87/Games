using Engine;
using System.Numerics;
using System.Text;

namespace MaskEdit
{
    internal class Generator
    {
        internal static void Basics()
        {
            var result = new StringBuilder();
            ulong mask = 1;
            for (int i = 0; i < Board.Size; i++)
            {
                result.AppendFormat(", 0x{0:X}", mask);
                mask <<= 1;
            }
            File.WriteAllText("Basics.txt", result.ToString());
        }

        internal static void Jumps()
        {
            var result = new StringBuilder();

            for (var x = 0; x < Board.Dimension; x++)
            {
                for (var y = 0; y < Board.Dimension; y++)
                {
                    ulong option = 0;
                    foreach (var o in Offset.Jumps)
                    {
                        var rank = x + o.Rank;
                        var file = y + o.File;

                        if (rank >= 0 && file >= 0 && rank < Board.Dimension && file < Board.Dimension)
                        {
                            var square = rank * Board.Dimension + file;
                            option |= BitSupport.Basic[square];
                        }
                    }

                    result.AppendFormat(", 0x{0:X}", option);
                }
            }

            File.WriteAllText("Jumps.txt", result.ToString());
        }

        internal static void Diagonals()
        {
            var result = new StringBuilder();

            for (var x = 0; x < Board.Dimension; x++)
            {
                for (var y = 0; y < Board.Dimension; y++)
                {
                    ulong option = 0;
                    foreach (var o in Offset.Diagonals)
                    {
                        var rank = x + o.Rank;
                        var file = y + o.File;

                        for (; rank >= 0 && file >= 0 && rank < Board.Dimension && file < Board.Dimension; rank += o.Rank, file += o.File)
                        {
                            var square = rank * Board.Dimension + file;
                            option |= BitSupport.Basic[square];
                        }
                    }

                    result.AppendFormat(", 0x{0:X}", option);
                }
            }

            File.WriteAllText("Diagonals.txt", result.ToString());
        }

        internal static void Straights()
        {
            var result = new StringBuilder();

            for (var x = 0; x < Board.Dimension; x++)
            {
                for (var y = 0; y < Board.Dimension; y++)
                {
                    ulong option = 0;
                    foreach (var o in Offset.Straights)
                    {
                        var rank = x + o.Rank;
                        var file = y + o.File;

                        for (; rank >= 0 && file >= 0 && rank < Board.Dimension && file < Board.Dimension; rank += o.Rank, file += o.File)
                        {
                            var square = rank * Board.Dimension + file;
                            option |= BitSupport.Basic[square];
                        }
                    }

                    result.AppendFormat(", 0x{0:X}", option);
                }
            }

            File.WriteAllText("Straights.txt", result.ToString());
        }

        internal static void Lines()
        {
            var result = new StringBuilder();

            foreach (var o in Offset.AllDirections)
            {
                result.Append(Environment.NewLine + ", new ulong[] {");
                for (var x = 0; x < Board.Dimension; x++)
                {
                    for(var y = 0; y < Board.Dimension; y++)
                    {
                        ulong option = 0;
                        var rank = x + o.Rank;
                        var file = y + o.File;

                        for (; rank >= 0 && file >= 0 && rank < Board.Dimension && file < Board.Dimension; rank += o.Rank, file += o.File)
                        {
                            var square = rank * Board.Dimension + file;
                            option |= BitSupport.Basic[square];
                        }
                        result.AppendFormat(", 0x{0:X}", option);
                    }
                }
            }

            File.WriteAllText("Lines.txt", result.ToString());
        }

        internal static void KingMoveOptions()
        {
            var result = new StringBuilder();
            for (var rank = 0; rank < Board.Dimension; rank++)
            {
                for (var file = 0; file < Board.Dimension; file++)
                {
                    ulong option = 0;
                    foreach (var o in Offset.AllDirections)
                    {
                        var row = rank + o.Rank;
                        var col = file + o.File;
                        if (row >= 0 && col >= 0 && row < Board.Dimension && col < Board.Dimension)
                        {
                            var square = row * Board.Dimension + col;
                            option |= BitSupport.Basic[square];
                        }
                    }
                    result.AppendFormat(", 0x{0:X}", option);
                }
            }
            File.WriteAllText("KingMoveOptions.txt", result.ToString());
        }
    }
}
