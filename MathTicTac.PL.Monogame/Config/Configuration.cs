using System;
using System.Configuration;

namespace Config
{
	public static class MathTicTacConfiguration
	{
		static MathTicTacConfiguration()
		{
			try
			{
				BigCellRowCount = Int32.Parse(ConfigurationManager.AppSettings["BigCellRowCount"]);
				BigCellColumnCount = Int32.Parse(ConfigurationManager.AppSettings["BigCellColumnCount"]);
				CellRowCount = Int32.Parse(ConfigurationManager.AppSettings["CellRowCount"]);
				CellColumnCount = Int32.Parse(ConfigurationManager.AppSettings["CellColumnCount"]);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Error in cctor {nameof(MathTicTacConfiguration)} class.", ex);
			}
		}

		public static readonly int BigCellRowCount;
		public static readonly int BigCellColumnCount;
		public static readonly int CellColumnCount;
		public static readonly int CellRowCount;

		public const int HEIGHT = 200;
		public const int WIDTH = 200;
	}
}