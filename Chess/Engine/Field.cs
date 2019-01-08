using Engine.Pieces;

namespace Engine
{
	public class Field
	{
		public byte File { get; }
		public byte Rank { get; }
		public Piece Piece { get; set; }
		
		public Field(byte file, byte rank)
		{
			// Fields are created without pieces on them.
			this.File = file;
			this.Rank = rank;
			this.Piece = null;
		}
		
		public override string ToString()
		{
			var pieceLetter = this.Piece == null ? "" : this.Piece.Letter().ToString();
			var fileLetter = (char)(this.File + Constants.FileLetterOffset);
			var rankNumber = (char)(this.Rank + Constants.RankNumberOffset);
			return pieceLetter + fileLetter + rankNumber;
		}
	}
}
