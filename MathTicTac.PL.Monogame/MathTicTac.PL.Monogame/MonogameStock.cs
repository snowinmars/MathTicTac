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
		public static Dictionary<VisibleState, Texture2D> cellsCrossTextures { get; internal set; }
		public static Dictionary<VisibleState, Texture2D> cellsZeroTextures { get; internal set; }

	}
}
