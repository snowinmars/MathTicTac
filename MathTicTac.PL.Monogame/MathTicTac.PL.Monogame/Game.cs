using Config;
using MathTicTac.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MathTicTac.PL.Monogame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	{
		private readonly GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private World world;
		private Texture2D crossCellTexture;
		private Texture2D zeroCellTexture;
		private Texture2D noneCellTexture;
		private Texture2D borderAllCellTexture;
		private Texture2D borderAllBigCellTexture;

		public Game()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
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

			this.world = new World(0);

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

			// TODO: use this.Content to load your game content here
		}

		private void LoadTexture()
		{
			crossCellTexture = this.Content.Load<Texture2D>("Textures/Cross");
			zeroCellTexture = this.Content.Load<Texture2D>("Textures/Zero");
			noneCellTexture = this.Content.Load<Texture2D>("Textures/None");
			borderAllCellTexture = this.Content.Load<Texture2D>("Textures/BorderCellAll");
			borderAllBigCellTexture = this.Content.Load<Texture2D>("Textures/BorderBigCellAll");
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

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			DrawWorld();

			spriteBatch.End();

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}

		private void DrawWorld()
		{
			DrawBigCells();
		}

		private void DrawBigCells()
		{
			Coord coord = new Coord();

			foreach (var item in this.world.BigCells)
			{
				spriteBatch.Draw(this.borderAllBigCellTexture, new Vector2(coord.X, coord.Y));

				DrawCells(item, coord);

				coord.X += MathTicTacConfiguration.BIGCELLWIDTH;

				if (coord.X + MathTicTacConfiguration.BIGCELLWIDTH > MathTicTacConfiguration.WORLDWIDTH)
				{
					coord.X = 0;
					coord.Y += MathTicTacConfiguration.BIGCELLHEIGHT;
				}
			}
		}

		private void DrawCells(BigCell bigCell, Coord bigCellCoord)
		{
			Coord coord = new Coord(bigCellCoord.X, bigCellCoord.Y);

			foreach (var item in bigCell.Cells)
			{
				spriteBatch.Draw(this.borderAllCellTexture, new Vector2(coord.X, coord.Y));

				if (MathTicTacConfiguration.Random.Next() % 2 == 0)
				{
					spriteBatch.Draw(this.crossCellTexture, new Vector2(coord.X, coord.Y));
				}
				else
				{
					spriteBatch.Draw(this.zeroCellTexture, new Vector2(coord.X, coord.Y));
				}

				coord.X += MathTicTacConfiguration.CELLWIDTH;

				if (coord.X + MathTicTacConfiguration.CELLWIDTH > bigCellCoord.X + MathTicTacConfiguration.BIGCELLWIDTH)
				{
					coord.X = bigCellCoord.X;
					coord.Y += MathTicTacConfiguration.CELLHEIGHT;
				}
			}
		}
	}
}