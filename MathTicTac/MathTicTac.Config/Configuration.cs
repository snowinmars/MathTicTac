using System;
using System.Configuration;

namespace Config
{
	/// <summary>
	/// All configs for app have to be here
	/// </summary>
	public static class Configuration
	{
		static Configuration()
		{
			// Try-catch block here is due to error in cctor is hard to detect without it
			try
			{
				Random = new Random();

				BigCellRowCount = Int32.Parse(ConfigurationManager.AppSettings["BigCellRowCount"]);
				BigCellColumnCount = Int32.Parse(ConfigurationManager.AppSettings["BigCellColumnCount"]);
				CellRowCount = Int32.Parse(ConfigurationManager.AppSettings["CellRowCount"]);
				CellColumnCount = Int32.Parse(ConfigurationManager.AppSettings["CellColumnCount"]);
				ServerUrl = ConfigurationManager.AppSettings["ServerUrl"];
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Error in cctor {nameof(Configuration)} class.", ex);
			}
		}

		public static string ServerUrl { get; set; }

		/// <summary>
		/// In all application programmer have to use only this ramdom examplar
		/// </summary>
		public static Random Random { get; set; }

		/// <summary>
		/// How many big cells is in the world's row
		/// </summary>
		public static readonly int BigCellRowCount;

		/// <summary>
		/// How many big cells is in the world's column
		/// </summary>
		public static readonly int BigCellColumnCount;

		/// <summary>
		/// How many small cells is in the big cell's column
		/// </summary>
		public static readonly int CellColumnCount;

		/// <summary>
		/// How many small cells is in the big cell's row
		/// </summary>
		public static readonly int CellRowCount;

		/// <summary>
		/// World height in pixels
		/// </summary>
		public const int WORLDHEIGHT = 500;

		/// <summary>
		/// World width in pixels
		/// </summary>
		public const int WORLDWIDTH = 500;

		/// <summary>
		/// Margin for cell
		/// </summary>
		public const int CELLSPRITEOFFSET = 5;

		/// <summary>
		/// Margin for big cell
		/// </summary>
		public const int BIGCELLSPRITEOFFSET = 5;

		/// <summary>
		/// Big cell's sprite size
		/// </summary>
		public const int BIGCELLHEIGHT = 160;

		/// <summary>
		/// Big cell's sprite size
		/// </summary>
		public const int BIGCELLWIDTH = 160;

		/// <summary>
		/// Cell's sprite size
		/// </summary>
		public const int CELLHEIGHT = 50;

		/// <summary>
		///  Cell's sprite size
		/// </summary>
		public const int CELLWIDTH = 50;
	}
}