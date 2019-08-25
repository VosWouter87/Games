using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace SetGame
{
	public class Caretaker
	{
		public static string FileDialogInitialDirectory = @"D:\Users\Leoni en Paul\Mjin Documenten\Wouter\saved games";
		public static string FileDialogFilter = "saved set games (*.set)|*.set";

		public GameState LoadState()
		{
			var openGameDialog = new OpenFileDialog();
			openGameDialog.InitialDirectory = FileDialogInitialDirectory;
			openGameDialog.Filter = FileDialogFilter;

			if (openGameDialog.ShowDialog() == DialogResult.OK)
			{
				using (var stream = openGameDialog.OpenFile())
				{
					var binaryFormatter = new BinaryFormatter();
					var state = binaryFormatter.Deserialize(stream) as GameState;

					if (state == null)
						throw new FileLoadException("File format was invalid.", openGameDialog.FileName);

					return state;
				}
			}

			return null;
		}

		public void SaveState(GameState state)
		{
			var saveGameDialog = new SaveFileDialog();
			saveGameDialog.InitialDirectory = FileDialogInitialDirectory;
			saveGameDialog.Filter = FileDialogFilter;

			if (saveGameDialog.ShowDialog() == DialogResult.OK)
			{
				using (var stream = saveGameDialog.OpenFile())
				{
					var binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(stream, state);
				}
			}
		}
	}
}
