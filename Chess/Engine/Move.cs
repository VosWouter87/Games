using System;

namespace Engine
{
	/// <summary>
	/// The following data is essential:
	/// Starting rank, file
	/// Ending rank, file
	/// The following data can be determined based on board situation:
	/// Color of the player that moved
	/// Whether a piece was struck
	/// Whether the opponent was checked or mated, this is left out in order to avoid the need for calculating it.
	/// </summary>
	public class Move
	{
		public byte StartFile { get; }
		public byte StartRank { get; }
		public byte EndFile { get; }
		public byte EndRank { get; }
		public bool White { get; }
		public bool Struck { get; }
		public KingStatus Checked { get; set; }
		public char Letter { get; }
		public CastlingOptions HasCastled { get; set; }

		public Move(int move)
		{
			this.StartFile = (byte)(move & 7);
			this.StartRank = (byte)((move & 56) >> 3);
			this.EndFile = (byte)((move & 448) >> 6);
			this.EndRank = (byte)((move & 3584) >> 9);
			this.White = (move & 4096) >> 12 == 1;
			this.Struck = (move & 8192) >> 13 == 1;
			this.Checked = (KingStatus)((move & 49152) >> 14);
			this.Letter = (char)((move & 8355840) >> 16);
		}

		/// <summary>
		/// Pass on both the starting rank and file and the ending, we don't want to do board analysis to determine the start.
		/// But we always need to do some board analysis, so pass on the board anyway.
		/// </summary>
		/// <param name="fullNotation">The position of the characters determine their meaning:
		/// 1. Starting file, noted by a letter a to f.
		/// 2. Starting rank, noted by a number 1 to 8.
		/// 3. An x if a piece was struck, nothing if there was no strike.
		/// 4. Ending file, noted by a letter a to f.
		/// 5. Ending rank, noted by a number 1 to 8.
		/// 6. In case of castling use another move constructor.
		/// 7. In case the king is checked add a +, if it's mated a #</param>
		public Move(string fullNotation, Board board)
		{
			if (fullNotation.Contains("0-0-0"))
			{
				this.HasCastled = board.WhiteToMove ? CastlingOptions.WhiteQueenSide : CastlingOptions.BlackQueenSide;
			}
			else if (fullNotation.Contains("0-0"))
			{
				this.HasCastled = board.WhiteToMove ? CastlingOptions.WhiteKingSide : CastlingOptions.BlackKingSide;
			}
			else
			{
				var notationIndex = 0;

				this.Letter = char.IsUpper(fullNotation[notationIndex]) ? fullNotation[notationIndex++] : ' ';

				this.StartFile = (byte)(fullNotation[notationIndex++] - Constants.FileLetterOffset);
				this.StartRank = (byte)(fullNotation[notationIndex++] - Constants.RankNumberOffset);

				var startField = board.Fields[this.StartFile, this.StartRank];

				if (startField.Piece == null)
				{
					throw new Exception("Invalid argument, no piece found at given location, file: " + this.StartFile + ", rank: " + this.StartRank);
				}

				this.White = startField.Piece.White;

				if (fullNotation[2] == 'x')
				{
					this.Struck = true;
					notationIndex++;
				}

				this.EndFile = (byte)(fullNotation[notationIndex++] - Constants.FileLetterOffset);
				this.EndRank = (byte)(fullNotation[notationIndex++] - Constants.RankNumberOffset);
			}
		}

		public Move(byte startFile, byte startRank, byte endFile, byte endRank, bool white, bool struck, KingStatus status, char letter)
		{
			this.StartFile = startFile;
			this.StartRank = startRank;
			this.EndFile = endFile;
			this.EndRank = endRank;
			this.White = white;
			this.Struck = struck;
			this.Checked = status;
			this.Letter = letter;
		}

		public int ToInt()
		{
			return this.StartFile + (this.StartRank << 3) + (this.EndFile << 6) + (this.EndRank << 9) + ((this.White ? 1 : 0) << 12) + ((this.Struck ? 1 : 0) << 13) + ((int)this.Checked << 14) + (this.Letter << 16);
		}

		public override string ToString()
		{
			return string.Format($"{(this.Struck ? "Strike by " : "Move ")} {this.Letter} from {(char)(this.StartFile + Constants.FileLetterOffset)}{this.StartRank + 1} to {(char)(this.EndFile + Constants.FileLetterOffset)}{this.EndRank + 1} {this.Checked}");
		}
	}
}
