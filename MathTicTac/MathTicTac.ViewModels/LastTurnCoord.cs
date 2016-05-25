using MathTicTac.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.ViewModels
{
	public class LastTurnCoord
	{
		public CoordServiceModel BigCellCoord { get; set; }
		public CoordServiceModel CellCoord { get; set; }
	}
}
