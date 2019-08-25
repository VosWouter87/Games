using System;
using System.Data;
using System.Windows.Forms;

namespace SetGame
{
	public static class Types
	{
		public static void SetDataSource(ListControl control, Type type)
		{
			var dataTable = new DataTable();
			if(type.IsEnum)
			{
				dataTable.Columns.Add("key", type);
				dataTable.Columns.Add("text", typeof(string));

				foreach (var value in Enum.GetValues(type))
					dataTable.Rows.Add(value, value.ToString());

				control.DataSource = dataTable;
				control.ValueMember = "key";
				control.DisplayMember = "text";
			}
		}
	}

	public enum Mode
	{
		Play,
		Edit
	}

	public enum Position
	{
		Stack,
		Play,
		Won
	}

	public enum Color
	{
		Red,
		Green,
		Purple,
	}

	public enum Shape
	{
		Elipse,
		Wave,
		Rect,
	}

	public enum Fill
	{
		Empty,
		Grain,
		Full
	}

	public enum Count
	{
		One = 1,
		Two = 2,
		Three = 3,
	}
}
