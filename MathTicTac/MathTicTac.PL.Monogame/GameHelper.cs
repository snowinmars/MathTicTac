namespace MathTicTac.PL.Monogame
{
	using Config;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System.Collections.Generic;
	using ViewModels;

	internal class GameHelper
	{
		public void Draw(CellViewModel cell, SpriteBatch bath)
		{
			switch (cell.State)
			{
				case State.None:
					bath.Draw(MonogameStock.noneCellTexture, cell.rectangle, Color.White);
					break;

				case State.Cross:
					bath.Draw(MonogameStock.cellsCrossTextures[cell.currentVisibleState], cell.rectangle, Color.White);
					break;

				case State.Zero:
					bath.Draw(MonogameStock.cellsZeroTextures[cell.currentVisibleState], cell.rectangle, Color.White);
					break;

				default:
					break;
			}
		}

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

			MonogameStock.borderAllBigCellFocusTexture = game.Content.Load<Texture2D>("Textures/BorderBigCellAllFocus");
			MonogameStock.borderAllBigCellTexture = game.Content.Load<Texture2D>("Textures/BorderBigCellAll");
			MonogameStock.crossBigCellTexture = game.Content.Load<Texture2D>("Textures/CrossBigCell");
			MonogameStock.zeroBigCellTexture = game.Content.Load<Texture2D>("Textures/ZeroBigCell");

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

		internal void SetCellsCoords(WorldServiceModel world)
		{
			Coord bigCellCoord = new Coord();

			for (int i = 0; i < world.BigCells.GetLength(0); i++)
				for (int j = 0; j < world.BigCells.GetLength(1); j++)
				{
					world.BigCells[i, j] = new BigCellServiceModel(State.None,
										true,
										new CellServiceModel[Configuration.CellRowCount, Configuration.CellColumnCount],
										bigCellCoord);

					Coord cellCoord = new Coord(bigCellCoord.X + Configuration.BIGCELLSPRITEOFFSET,
									bigCellCoord.Y + Configuration.BIGCELLSPRITEOFFSET);

					for (int e = 0; e < world.BigCells[i, j].Cells.GetLength(0); e++)
						for (int k = 0; k < world.BigCells[i, j].Cells.GetLength(1); k++)
						{
							world.BigCells[i, j].Cells[e, k] = new CellServiceModel(State.None);

							cellCoord.X += Configuration.CELLWIDTH;

							if (cellCoord.X + Configuration.CELLWIDTH > bigCellCoord.X + Configuration.BIGCELLWIDTH)
							{
								cellCoord.X = bigCellCoord.X + Configuration.BIGCELLSPRITEOFFSET;
								cellCoord.Y += Configuration.CELLHEIGHT;
							}
						}

					bigCellCoord.X += Configuration.BIGCELLWIDTH;

					if (bigCellCoord.X + Configuration.BIGCELLWIDTH > Configuration.WORLDWIDTH)
					{
						bigCellCoord.X = 0;
						bigCellCoord.Y += Configuration.BIGCELLHEIGHT;
					}
				}
		}

		private bool turn;

		internal void OnMouseClickCrunch(WorldServiceModel world, Coord bigCellCoord, Coord cellCoord)
		{
			BigCellServiceModel bigcell = world.BigCells[bigCellCoord.X, bigCellCoord.Y];
			CellServiceModel cell = world.BigCells[bigCellCoord.X, bigCellCoord.Y].Cells[cellCoord.X, cellCoord.Y];

			if (bigcell.IsFocus)
			{
				if (cell.State == State.None)
				{
					world.SetAllBigCellsToState(false);
					world.BigCells[cellCoord.X, cellCoord.Y].IsFocus = true;

					if (turn)
					{
						cell.State = State.Cross;
					}
					else
					{
						cell.State = State.Zero;
					}

					turn = !turn;

					if (bigcell.IsFilled())
					{
						world.SetAllBigCellsToState(true);
					}
				}
			}
		}
	}
}