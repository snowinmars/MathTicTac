using MathTicTac.Entities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.PL.Monogame
{
	public static class MonogameStock
	{
		public static Dictionary<VisibleState, Texture2D> cellsTextures { get; internal set; }

	}
}
