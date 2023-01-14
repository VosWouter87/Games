using System.Net.WebSockets;

namespace Engine
{
	public class Square
	{
		public byte File { get; }
		public byte Rank { get; }
		public int Index { get; }
		public ulong Mask { get; }
		public PieceType Piece { get; set; }
		
		public Square(Board board, byte file, byte rank)
		{
			// Fields are created without pieces on them.
			this.File = file;
			this.Rank = rank;
			this.Index = file + rank << 3;
			this.Mask = BitSupport.Basic[this.Index];
			this.Piece = board.GetPieceType(this.Mask);
		}
		
/*		public override string ToString()
		{
			var pieceLetter = this.Piece == null ? "" : this.Piece.Letter().ToString();
			var fileLetter = (char)(this.File + Constants.FileLetterOffset);
			var rankNumber = (char)(this.Rank + Constants.RankNumberOffset + 1);
			return pieceLetter + fileLetter + rankNumber;
		}
*/
	}
}
