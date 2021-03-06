﻿namespace MathTicTac.PL.Monogame
{
	using Config;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System.Collections.Generic;
	using System;
	using Enums;
	using ViewModels;
	using ServiceModels;
	using Microsoft.Xna.Framework.Input;
	internal class GameHelper
	{
		public void Draw(CellViewModel cell, SpriteBatch bath)
		{
			switch (cell.State)
			{
				case State.None:
					bath.Draw(MonogameStock.noneCellTexture, cell.rectangle, Color.White);
					break;

				case State.Client:
					bath.Draw(MonogameStock.cellsCrossTextures[cell.currentVisibleState], cell.rectangle, Color.White);
					break;

				case State.Enemy:
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

		internal void SetCellsCoords(WorldViewModel world)
		{
			CoordServiceModel bigCellCoord = new CoordServiceModel();

			for (int i = 0; i < world.BigCells.GetLength(0); i++)
				for (int j = 0; j < world.BigCells.GetLength(1); j++)
				{
					world.BigCells[i, j] = new BigCellViewModel(State.None,
										true,
										new CellViewModel[Configuration.CellRowCount, Configuration.CellColumnCount],
										bigCellCoord);

					CoordServiceModel cellCoord = new CoordServiceModel(bigCellCoord.X + Configuration.BIGCELLSPRITEOFFSET,
									bigCellCoord.Y + Configuration.BIGCELLSPRITEOFFSET);

					for (int e = 0; e < world.BigCells[i, j].Cells.GetLength(0); e++)
						for (int k = 0; k < world.BigCells[i, j].Cells.GetLength(1); k++)
						{
							world.BigCells[i, j].Cells[e, k] = new CellViewModel(State.None, world.BigCells[i,j].Position, 0,0);

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

		internal void OnMouseClickCrunch(WorldViewModel world, CoordServiceModel bigCellCoord, CoordServiceModel cellCoord)
		{
			BigCellViewModel bigcell = world.BigCells[bigCellCoord.X, bigCellCoord.Y];
			CellViewModel cell = world.BigCells[bigCellCoord.X, bigCellCoord.Y].Cells[cellCoord.X, cellCoord.Y];

			if (bigcell.IsFocus)
			{
				if (cell.State == State.None)
				{
					world.SetAllBigCellsToState(false);
					world.BigCells[cellCoord.X, cellCoord.Y].IsFocus = true;

					if (turn)
					{
						cell.State = State.Client;
					}
					else
					{
						cell.State = State.Enemy;
					}

					turn = !turn;

					if (bigcell.IsFilled())
					{
						world.SetAllBigCellsToState(true);
					}
				}
			}
		}

		internal void Update(CellViewModel cell)
		{
			cell.previousVisibleState = cell.currentVisibleState;
			cell.previousMouseState = cell.currentMouseState;

			cell.currentMouseState = Mouse.GetState();

			if (cell.rectangle.Contains(cell.currentMouseState.X, cell.currentMouseState.Y))
			{
				if (cell.currentMouseState.LeftButton == ButtonState.Pressed)
				{
					if (cell.previousMouseState.LeftButton == ButtonState.Released)
					{
						cell.OnMouseDown(EventArgs.Empty);
						cell.currentVisibleState = VisibleState.Pressed;
					}
					else
					{
						if (cell.currentVisibleState != VisibleState.Pressed)
						{
							cell.currentVisibleState = VisibleState.Hover;
						}
					}
				}
				else
				{
					if (cell.previousVisibleState == VisibleState.Pressed)
					{
						cell.OnMouseClick(null);
					}

					cell.currentVisibleState = VisibleState.Hover;
				}
			}
			else
			{
				if (cell.previousVisibleState == VisibleState.Hover ||
					cell.previousVisibleState == VisibleState.Pressed)
				{
					cell.OnMouseOut(EventArgs.Empty);
				}

				cell.currentVisibleState = VisibleState.Normal;
			}

			if (cell.currentMouseState.LeftButton == ButtonState.Released &&
				cell.previousVisibleState == VisibleState.Pressed)
			{
				cell.OnMouseUp(EventArgs.Empty);
			}
		}
	}
}