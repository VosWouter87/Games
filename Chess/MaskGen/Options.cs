using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskGen
{
    internal class Options
    {
        public static ulong[] KnightMasks()
        {
            var result = new ulong[Board.Size];
            for (var x = 0; x < Board.Dimension; x++)
            {
                for (var y = 0; y < Board.Dimension; y++)
                {
                    ulong option = 0;
                    foreach (var o in Offset.Knight)
                    {
                        var rank = x + o.Rank;
                        var file = y + o.File;

                        if (rank >= 0 && file >= 0 && rank < Board.Dimension && file < Board.Dimension)
                        {
                            //Constants.
                        }
                    }

                    var index = x * Board.Dimension + y;
                    result[index++] = option;
                }
            }
            return result;
        }


    }
}
