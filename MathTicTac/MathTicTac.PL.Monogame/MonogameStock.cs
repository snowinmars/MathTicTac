namespace MathTicTac.PL.Monogame
{
	using Enums;
	using Microsoft.Xna.Framework.Graphics;
	using ServiceModels;
	using System.Collections.Generic;
	public static class MonogameStock
	{
		public static Dictionary<VisibleState, Texture2D> cellsCrossTextures { get; internal set; }
		public static Dictionary<VisibleState, Texture2D> cellsZeroTextures { get; internal set; }
		internal static Texture2D borderAllBigCellTexture;
		internal static Texture2D borderAllCellTexture;
		internal static Texture2D crossCellHoverTexture;
		internal static Texture2D crossCellNormalTexture;
		internal static Texture2D crossCellPressedTexture;
		internal static Texture2D noneCellTexture;
		internal static Texture2D zeroCellHoverTexture;
		internal static Texture2D zeroCellNormalTexture;
		internal static Texture2D zeroCellPressedTexture;
		internal static Texture2D borderAllBigCellFocusTexture;
		internal static Texture2D zeroBigCellTexture;
		internal static Texture2D crossBigCellTexture;
	}
}