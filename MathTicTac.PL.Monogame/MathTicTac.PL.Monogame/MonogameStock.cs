﻿namespace MathTicTac.PL.Monogame
{
	using DTO;
	using Microsoft.Xna.Framework.Graphics;
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
	}
}