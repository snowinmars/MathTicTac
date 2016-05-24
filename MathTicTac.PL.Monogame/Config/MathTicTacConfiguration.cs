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
                Random = new Random();

                BigCellRowCount = Int32.Parse(ConfigurationManager.AppSettings["BigCellRowCount"]);
                BigCellColumnCount = Int32.Parse(ConfigurationManager.AppSettings["BigCellColumnCount"]);
                CellRowCount = Int32.Parse(ConfigurationManager.AppSettings["CellRowCount"]);
                CellColumnCount = Int32.Parse(ConfigurationManager.AppSettings["CellColumnCount"]);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error in ctor {nameof(MathTicTacConfiguration)} class.", ex);
            }
        }

        public static Random Random;

        public static readonly int BigCellRowCount;
        public static readonly int BigCellColumnCount;
        public static readonly int CellColumnCount;
        public static readonly int CellRowCount;

        public const int WORLDHEIGHT = 500;
        public const int WORLDWIDTH = 500;

        public const int BIGCELLHEIGHT = 160;
        public const int BIGCELLWIDTH = 160;

        public const int CELLHEIGHT = 50;
        public const int CELLWIDTH = 50;

        public const int tokenExpirationDays = 15;
	}
}