using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quixx {
  internal class Card {
    public const int Colors = 4;
    public const int Numbers = 10;
    public long checks;

    public bool GetCheck (int color, int number, bool set) {
      if (color < 0 || color >= Colors || number < 0 || number > Numbers) {
        throw new ArgumentException($"Invalid indexes for get color: {color}, number: {number}");
      }
      return ((1 << (color * number)) & checks) == 1;

    } // GetCheck

    public void SetCheck (int color, int number, bool set) {
      if (color < 0 || color >= Colors || number < 0 || number > Numbers) {
        throw new ArgumentException($"Invalid indexes for assignment color: {color}, number: {number}");
      }
      var mask = 1 >> color & number;
      if (set) {
        checks &= mask;
      } else {
        checks &= ~mask;
      }
    } // SetCheck
  } // Class Card
} // Namespace Quixx
