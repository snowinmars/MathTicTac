namespace MathTicTac.PL.Monogame
{
	using Config;
	using Enums;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;
	using ServiceModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using ViewModels;

	internal class GameHelper
	{
		private bool turn;

		public Texture2D CreateTexture(GraphicsDevice device, int width, int height, Color color, byte transperity = byte.MaxValue)
		{
			Texture2D texture = new Texture2D(device, width, height);
			Color[] data = new Color[width * height];

			for (int i = 0; i < data.Length; i++)
			{
				data[i] = color;
			}

			texture.SetData(data);

			return texture;
		}

		public Texture2D CreateTexture(GraphicsDevice device, int width, int height, Color textureColor, int borderThick, Color borderColor)
		{
			Texture2D texture = new Texture2D(device, width, height);

			Color[] data = new Color[width * height];

			for (int i = 0; i < data.Length; i++)
			{
				data[i] = textureColor;
			}

			// painting vertical borders

			for (int i = 0; i < data.Length; i = i + width)
			{
				for (int j = 0; j < borderThick; j++)
				{
					data[i + j] = borderColor;
				}

				if (i > 1)
				{
					for (int j = 0; j < borderThick; j++)
					{
						data[i - 1 - j] = borderColor;
					}
				}
			}

			// painting horisontal borders

			for (int j = 0; j < borderThick; j++)
			{
				var bias = j * width;

				for (int i = 0; i < height; i++)
				{
					data[i + bias] = borderColor;
					data[data.Length - i - 1 - j * width] = borderColor;
				}
			}

			//set the color
			texture.SetData(data);

			return texture;
		}

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

			MonogameStock.crossBigCellTexture = game.Content.Load<Texture2D>("Textures/CrossBigCell");
			MonogameStock.zeroBigCellTexture = game.Content.Load<Texture2D>("Textures/ZeroBigCell");

			MonogameStock.noneCellTexture = this.CreateTexture(game.GraphicsDevice, 40, 40, Color.White, 1, Color.Black);
			MonogameStock.cellLastTurnTexture = this.CreateTexture(game.GraphicsDevice, 50, 50, Color.Orange, 64);

			MonogameStock.borderAllCellTexture = this.CreateTexture(game.GraphicsDevice, 50, 50, Color.White, 1, Color.Black);
			MonogameStock.borderAllBigCellFocusTexture = this.CreateTexture(game.GraphicsDevice, 160, 160, Color.White, 4, Color.SkyBlue);
			MonogameStock.borderAllBigCellTexture = this.CreateTexture(game.GraphicsDevice, 160, 160, Color.White, 1, Color.Black);


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

		internal void MonogameStockUnload()
		{
			//MonogameStock.gameService.Close();
			//MonogameStock.accountService.Close();
		}

		internal void OnMouseClickCrunch(WorldViewModel world, CoordServiceModel bigCellCoord, CoordServiceModel cellCoord)
		{
			BigCellViewModel bigcell = world.BigCells[bigCellCoord.X, bigCellCoord.Y];
			CellViewModel cell = world.BigCells[bigCellCoord.X, bigCellCoord.Y].Cells[cellCoord.X, cellCoord.Y];

			if (bigcell.IsFocus)
			{
				if (cell.State == State.None)
				{
					world.LastTurnCoord = new LastTurnCoord
					{
						BigCellCoord = new CoordServiceModel((int)bigcell.Position.X, (int)bigcell.Position.Y),
						CellCoord = new CoordServiceModel((int)cell.Position.X, (int)cell.Position.Y),
					};

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

		internal async void Login(GameAccountViewModel clienAccount)
		{
			string username = "snowinmars";
			string password = "Novikova";
			int id = 13;

			//clienAccount.Id = id;
			//clienAccount.Password = password;
			//clienAccount.Username = username;

			//await MyHttpClient.PostAsync<GameAccountViewModel>(clienAccount, "Account").ConfigureAwait(false);
		}

		internal void SetCellsCoords(WorldViewModel world)
		{
			CoordServiceModel bigCellCoord = new CoordServiceModel(0, 0);

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
							world.BigCells[i, j].Cells[e, k] = new CellViewModel(State.None,
													cellCoord,
													Configuration.CELLWIDTH,
													Configuration.CELLHEIGHT);
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

		private void Call(string userName, string password, string controllerName, WebMethod post)
		{
			throw new NotImplementedException();
		}
	}
}