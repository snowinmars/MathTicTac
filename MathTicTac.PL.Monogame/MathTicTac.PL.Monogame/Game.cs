using Config;
using MathTicTac.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MathTicTac.PL.Monogame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	{
		private readonly GraphicsDeviceManager graphics;
		private Texture2D borderAllBigCellTexture;
		private Texture2D borderAllCellTexture;
		private Texture2D crossCellHoverTexture;
		private Texture2D crossCellNormalTexture;
		private Texture2D crossCellPressedTexture;
		private Texture2D noneCellTexture;
		private SpriteBatch spriteBatch;
		private World world;
		private Texture2D zeroCellHoverTexture;
		private Texture2D zeroCellNormalTexture;
		private Texture2D zeroCellPressedTexture;

		public Game()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			foreach (var bigcell in this.world.BigCells)
			{
				spriteBatch.Draw(this.borderAllBigCellTexture, bigcell.position, Color.White);

				foreach (var cell in bigcell.Cells)
				{
					cell.Draw(spriteBatch);
				}
			}

			spriteBatch.End();

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			this.world = new World(0, new BigCell[MathTicTacConfiguration.BigCellRowCount, MathTicTacConfiguration.BigCellColumnCount]);
			this.IsMouseVisible = true;

			Coord bigCellCoord = new Coord();

			for (int i = 0; i < this.world.BigCells.GetLength(0); i++)
				for (int j = 0; j < this.world.BigCells.GetLength(1); j++)
				{
					this.world.BigCells[i, j] = new BigCell(State.None, false, new Cell[MathTicTacConfiguration.CellRowCount, MathTicTacConfiguration.CellColumnCount], new Vector2(bigCellCoord.X, bigCellCoord.Y));

					Coord cellCoord = new Coord(bigCellCoord.X + MathTicTacConfiguration.BIGCELLSPRITEOFFSET, bigCellCoord.Y + MathTicTacConfiguration.BIGCELLSPRITEOFFSET);

					for (int e = 0; e < this.world.BigCells[i, j].Cells.GetLength(0); e++)
						for (int k = 0; k < this.world.BigCells[i, j].Cells.GetLength(1); k++)
						{
							this.world.BigCells[i, j].Cells[e, k] = new Cell(State.None, new Vector2(cellCoord.X, cellCoord.Y), MathTicTacConfiguration.CELLWIDTH, MathTicTacConfiguration.CELLHEIGHT);

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

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			LoadTexture();

			MonogameStockLoad();

			//Dictionary<VisibleState, Texture2D> cellstextures = new Dictionary<VisibleState, Texture2D>();

			foreach (var bigcell in this.world.BigCells)
			{
				foreach (var cell in bigcell.Cells)
				{
					cell.SetTextures(MonogameStock.cellsZeroTextures);
				}
			}

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			foreach (var bigcell in this.world.BigCells)
			{
				foreach (var cell in bigcell.Cells)
				{
					cell.Update();
				}
			}

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		private void LoadTexture()
		{
			zeroCellNormalTexture = this.Content.Load<Texture2D>("Textures/ZeroNormal");
			zeroCellHoverTexture = this.Content.Load<Texture2D>("Textures/ZeroHover");
			zeroCellPressedTexture = this.Content.Load<Texture2D>("Textures/ZeroPressed");

			crossCellNormalTexture = this.Content.Load<Texture2D>("Textures/CrossNormal");
			crossCellHoverTexture = this.Content.Load<Texture2D>("Textures/CrossHover");
			crossCellPressedTexture = this.Content.Load<Texture2D>("Textures/CrossPressed");

			noneCellTexture = this.Content.Load<Texture2D>("Textures/None");

			borderAllCellTexture = this.Content.Load<Texture2D>("Textures/BorderCellAll");
			borderAllBigCellTexture = this.Content.Load<Texture2D>("Textures/BorderBigCellAll");
		}

		private void MonogameStockLoad()
		{
			MonogameStock.cellsCrossTextures = new Dictionary<VisibleState, Texture2D>
			{
				{ VisibleState.Hover, this.crossCellHoverTexture},
				{ VisibleState.Normal, this.crossCellNormalTexture},
				{ VisibleState.Pressed, this.crossCellPressedTexture},
			};

			MonogameStock.cellsZeroTextures = new Dictionary<VisibleState, Texture2D>
			{
				{ VisibleState.Hover, this.zeroCellHoverTexture },
				{ VisibleState.Normal, this.zeroCellNormalTexture},
				{ VisibleState.Pressed, this.zeroCellPressedTexture},
			};
		}
	}
}