namespace MathTicTac.PL.Monogame.DTO
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;
	using System;
	using System.Collections.Generic;

	public class Button
	{
		protected readonly string buttonText;
		protected readonly Vector2 position;
		protected readonly Rectangle rectangle;
		protected Dictionary<VisibleState, Texture2D> textures = new Dictionary<VisibleState, Texture2D> { { VisibleState.Normal, MonogameStock.noneCellTexture }, };
		protected VisibleState currentVisibleState = VisibleState.Normal;
		protected VisibleState previousVisibleState = VisibleState.Normal;
		protected MouseState currentMouseState;
		protected MouseState previousMouseState;

		public Button(Vector2 position, int width, int height, string buttonText = "")
		{
			this.position = position;
			this.buttonText = buttonText;
			this.rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
		}

		public void SetTextures(Dictionary<VisibleState, Texture2D> dict)
		{
			this.textures = dict; // due to I wanna all cells have same copy of textures.
		}

		public event EventHandler MouseClick;

		public event EventHandler MouseDown;

		public event EventHandler MouseOut;

		public event EventHandler MouseUp;

		public SpriteFont Font { get; set; }

		public Dictionary<VisibleState, Texture2D> Textures
		{
			get { return this.textures; }
		}

		public virtual void Draw(SpriteBatch bath)
		{
			bath.Draw(this.textures[currentVisibleState], this.rectangle, Color.White);
			//bath.DrawString(Font, _buttonText, _position, Microsoft.Xna.Framework.Color.Black);
		}

		public void Update()
		{
			this.previousVisibleState = this.currentVisibleState;
			this.previousMouseState = this.currentMouseState;

			this.currentMouseState = Mouse.GetState();

			if (this.rectangle.Contains(this.currentMouseState.X, this.currentMouseState.Y))
			{
				if (this.currentMouseState.LeftButton == ButtonState.Pressed)
				{
					if (this.previousMouseState.LeftButton == ButtonState.Released)
					{
						this.OnMouseDown(EventArgs.Empty);
						this.currentVisibleState = VisibleState.Pressed;
					}
					else
					{
						if (this.currentVisibleState != VisibleState.Pressed)
						{
							this.currentVisibleState = VisibleState.Hover;
						}
					}
				}
				else
				{
					if (this.previousVisibleState == VisibleState.Pressed)
					{
						this.OnMouseClick(EventArgs.Empty);
					}

					this.currentVisibleState = VisibleState.Hover;
				}
			}
			else
			{
				if (this.previousVisibleState == VisibleState.Hover ||
					this.previousVisibleState == VisibleState.Pressed)
				{
					this.OnMouseOut(EventArgs.Empty);
				}

				this.currentVisibleState = VisibleState.Normal;
			}

			if (this.currentMouseState.LeftButton == ButtonState.Released &&
				this.previousVisibleState == VisibleState.Pressed)
			{
				this.OnMouseUp(EventArgs.Empty);
			}
		}

		private void OnMouseClick(EventArgs e)
		{
			this.MouseClick?.Invoke(this, e);
		}

		private void OnMouseDown(EventArgs e)
		{
			this.MouseDown?.Invoke(this, e);
		}

		private void OnMouseOut(EventArgs e)
		{
			this.MouseOut?.Invoke(this, e);
		}

		private void OnMouseUp(EventArgs e)
		{
			this.MouseUp?.Invoke(this, e);
		}
	}
}