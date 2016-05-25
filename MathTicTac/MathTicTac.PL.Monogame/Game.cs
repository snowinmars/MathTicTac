namespace MathTicTac.PL.Monogame
{
	using Config;
	using Enums;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;
	using ServiceModels;
	using System;
	using ViewModels;

/// <summary>
			/// This is the main type for your game.
			/// </summary>
	internal class Game : Microsoft.Xna.Framework.Game
	{
		private static SpriteBatch spriteBatch;
		private readonly GraphicsDeviceManager graphics;

		private GameHelper gameHelper;
		private WorldViewModel world;

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
			//GraphicsDevice.Clear(Color.CornflowerBlue);

			Game.spriteBatch.Begin();

			foreach (var bigcell in this.world.BigCells)
			{
				if (bigcell.IsFocus)
				{
					Game.spriteBatch.Draw(MonogameStock.borderAllBigCellFocusTexture, new Vector2(bigcell.Position.X, bigcell.Position.Y), Color.White);
				}
				else
				{
					Game.spriteBatch.Draw(MonogameStock.borderAllBigCellTexture, new Vector2(bigcell.Position.X, bigcell.Position.Y), Color.White);
				}

				foreach (var cell in bigcell.Cells)
				{
					this.gameHelper.Draw(cell, Game.spriteBatch);
				}

				switch (bigcell.State)
				{
					case State.None:
						break;

					case State.Client:
						Game.spriteBatch.Draw(MonogameStock.crossBigCellTexture, new Vector2(bigcell.Position.X, bigcell.Position.Y), Color.White);
						break;

					case State.Enemy:
						Game.spriteBatch.Draw(MonogameStock.zeroBigCellTexture, new Vector2(bigcell.Position.X, bigcell.Position.Y), Color.White);
						break;

					default:
						throw new ArgumentException($"Enum {nameof(State)} is invalid");
				}
			}

			Game.spriteBatch.End();

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
			this.gameHelper = new GameHelper();

			this.world = new WorldViewModel(0, new BigCellViewModel[Configuration.BigCellRowCount, Configuration.BigCellColumnCount]); // TODO map from logic
			this.IsMouseVisible = true;

			this.gameHelper.SetCellsCoords(this.world);

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			Game.spriteBatch = new SpriteBatch(this.GraphicsDevice);

			this.gameHelper.MonogameStockLoad(this);

			for (int i = 0; i < this.world.BigCells.GetLength(0); i++)
				for (int j = 0; j < this.world.BigCells.GetLength(1); j++)
					for (int e = 0; e < this.world.BigCells[i, j].Cells.GetLength(0); e++)
						for (int k = 0; k < this.world.BigCells[i, j].Cells.GetLength(1); k++)
						{
							var cell = this.world.BigCells[i, j].Cells[e, k];
							var bigcell = this.world.BigCells[i, j];

							cell.SetTextures(MonogameStock.cellsZeroTextures);

							int NonClosure_i = i;
							int NonClosure_j = j;
							int NonClosure_e = e;
							int NonClosure_k = k;

							// TODO how to run out of closure?

							cell.MouseClick += (o, s) =>
							{
								this.gameHelper.OnMouseClickCrunch(this.world,
													new CoordServiceModel(NonClosure_i, NonClosure_j),
													new CoordServiceModel(NonClosure_e, NonClosure_k));
							};
						}
			// TODO: use this.Content to load your game content here
			base.LoadContent();
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
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			foreach (var bigcell in this.world.BigCells)
			{
				foreach (var cell in bigcell.Cells)
				{
					this.gameHelper.Update(cell);
				}
			}

			// TODO: Add your update logic here

			base.Update(gameTime);
		}
	}
}