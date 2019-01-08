using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Pieces
{
	public class AttackCombo
	{
		public King King { get; set; }
		public Piece Pinned { get; set; }
		public Piece Attacker { get; set; }
		public int KingRank { get; set; }
		public int KingFile { get; set; }
		public int PinnedRank { get; set; }
		public int PinnedFile { get; set; }
		public int AttackerRank { get; set; }
		public int AttackerFile { get; set; }
	}
}
