// See https://aka.ms/new-console-template for more information
using Dice;

Console.WriteLine("Rolls with at least one occurence of the number:");
var occurences = Rolls.GetOccurences();
for (var i = 0; i < occurences.Length; i++) {
  Console.WriteLine((i + 2).ToString() + ": " + (occurences[i]).ToString());
}
const double maxRolls = 1296;

var candidates = new int[][] {
  new[] { 6, 7, 8 },
  new[] { 5, 7, 8 },
  new[] { 6, 7, 9 },
  new[] { 4, 7, 8 },
  new[] { 6, 7, 10 },
  new[] { 6, 5, 8 },
  new[] { 6, 9, 8 },
  new[] { 6, 7 },
  new[] { 7, 8 },
};
foreach (var candidate in candidates) {
  var goodRolls = Rolls.SuccessCountForCandidates(new List<int>(candidate));
  Console.WriteLine("Good rolls: " + goodRolls);
  Console.WriteLine(String.Join(",", candidate) + " success chance: " + ((double)goodRolls) / maxRolls * 100d);
}

Console.ReadLine();
