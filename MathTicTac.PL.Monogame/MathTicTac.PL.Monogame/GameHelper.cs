namespace MathTicTac.PL.Monogame
{
	using Config;
	using DTO;
	using Entities;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System;
	using System.Collections.Generic;

	internal class GameHelper
	{
		internal void MonogameStockLoad(Game game)
		{
			MonogameStock.zeroCellNormalTexture = game.Content.Load<Texture2D>("Textures/ZeroNormal");
			MonogameStock.zeroCellHoverTexture = game.Content.Load<Texture2D>("Textures/ZeroHover");
			MonogameStock.zeroCellPressedTexture = game.Content.Load<Texture2D>("Textures/ZeroPressed");

			MonogameStock.crossCellNormalTexture = game.Content.Load<Texture2D>("Textures/CrossNormal");
			MonogameStock.crossCellHoverTexture = game.Content.Load<Texture2D>("Textures/CrossHover");
			MonogameStock.crossCellPressedTexture = game.Content.Load<Texture2D>("Textures/CrossPressed");

			MonogameStock.noneCellTexture = game.Content.Load<Texture2D>("Textures/None");

			MonogameStock.borderAllCellTexture = game.Content.Load<Texture2D>("Textures/BorderCellAll");
			MonogameStock.borderAllBigCellTexture = game.Content.Load<Texture2D>("Textures/BorderBigCellAll");

			MonogameStock.cellsCrossTextures = new Dictionary<VisibleState, Texture2D>
			{
				{ VisibleState.Hover, MonogameStock.crossCellHoverTexture},
				{ VisibleState.Normal, MonogameStock.crossCellNormalTexture},
				{ VisibleState.Pressed, MonogameStock.crossCellPressedTexture},
			};

			MonogameStock.cellsZeroTextures = new Dictionary<VisibleState, Texture2D>
			{
				{ VisibleState.Hover, MonogameStock.zeroCellHoverTexture },
				{ VisibleState.Normal, MonogameStock.zeroCellNormalTexture},
				{ VisibleState.Pressed, MonogameStock.zeroCellPressedTexture},
			};
		}

		internal void SetCellsCoords(WorldDTO world)
		{
			Coord bigCellCoord = new Coord();

			for (int i = 0; i < world.BigCells.GetLength(0); i++)
				for (int j = 0; j < world.BigCells.GetLength(1); j++)
				{
					world.BigCells[i, j] = new BigCellDTO(State.None,
										true,
										new CellDTO[MathTicTacConfiguration.CellRowCount, MathTicTacConfiguration.CellColumnCount],
										new Vector2(bigCellCoord.X, bigCellCoord.Y));

					Coord cellCoord = new Coord(bigCellCoord.X + MathTicTacConfiguration.BIGCELLSPRITEOFFSET,
									bigCellCoord.Y + MathTicTacConfiguration.BIGCELLSPRITEOFFSET);

					for (int e = 0; e < world.BigCells[i, j].Cells.GetLength(0); e++)
						for (int k = 0; k < world.BigCells[i, j].Cells.GetLength(1); k++)
						{
							world.BigCells[i, j].Cells[e, k] = new CellDTO(State.None,
													new Vector2(cellCoord.X, cellCoord.Y),
													MathTicTacConfiguration.CELLWIDTH,
													MathTicTacConfiguration.CELLHEIGHT);

							cellCoord.X += MathTicTacConfiguration.CELLWIDTH;

							if (cellCoord.X + MathTicTacConfiguration.CELLWIDTH > bigCellCoord.X + MathTicTacConfiguration.BIGCELLWIDTH)
							{
								cellCoord.X = bigCellCoord.X + MathTicTacConfiguration.BIGCELLSPRITEOFFSET;
								cellCoord.Y += MathTicTacConfiguration.CELLHEIGHT;
							}
						}

					bigCellCoord.X += MathTicTacConfiguration.BIGCELLWIDTH;

					if (bigCellCoord.X + MathTicTacConfiguration.BIGCELLWIDTH > MathTicTacConfiguration.WORLDWIDTH)
					{
						bigCellCoord.X = 0;
						bigCellCoord.Y += MathTicTacConfiguration.BIGCELLHEIGHT;
					}
				}
		}

		internal void OnMouseClickCrunch(WorldDTO world, Coord bigCellCoord, Coord cellCoord)
		{
			BigCellDTO bigcell = world.BigCells[bigCellCoord.X, bigCellCoord.Y];
			CellDTO cell = world.BigCells[bigCellCoord.X, bigCellCoord.Y].Cells[cellCoord.X, cellCoord.Y];

			if (bigcell.IsFocus)
			{
				world.SetAllBigCellsToState(false);
				world.BigCells[cellCoord.X, cellCoord.Y].IsFocus = true;

				if (MathTicTacConfiguration.Random.Next() % 2 == 0)
				{
					cell.State = State.Client;
				}
				else
				{
					cell.State = State.Enemy;
				}
			}
		}
	}
}