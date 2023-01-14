using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    internal class Player
    {
        internal bool White { get; private set; }
        internal int KingLocation { get; private set; }
        internal ulong Pieces { get; private set; }


        public Player(bool white)
        {
            White = white;
        }
    }
}
