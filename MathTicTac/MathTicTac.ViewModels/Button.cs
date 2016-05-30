namespace MathTicTac.ViewModels
{
	using Enums;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;
	using System;
	using System.Collections.Generic;

	public class Button
	{
		/// <summary>
		/// Now is not work
		/// </summary>
		public readonly string buttonText;

		/// <summary>
		/// Position in pixels
		/// </summary>
		public readonly Vector2 Position;

		/// <summary>
		/// Physic frame
		/// </summary>
		public readonly Rectangle rectangle;

		/// <summary>
		/// Init it with MonogameStock dictionaries, using SetTextures() method
		/// </summary>
		public Dictionary<VisibleState, Texture2D> textures = new Dictionary<VisibleState, Texture2D>();

		public VisibleState currentVisibleState = VisibleState.Normal;
		public VisibleState previousVisibleState = VisibleState.Normal;
		public MouseState currentMouseState;
		public MouseState previousMouseState;

		public Button(Vector2 position, int width, int height, string buttonText = "")
		{
			this.Position = position;
			this.buttonText = buttonText;
			this.rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
		}

		/// <summary>
		/// Set textures with this methods.
		/// </summary>
		/// <param name="dict"></param>
		public void SetTextures(Dictionary<VisibleState, Texture2D> dict)
		{
			this.textures = dict; // due to I wanna all cells have same copy of textures.
		}

		public event EventHandler MouseClick;

		public event EventHandler MouseDown;

		public event EventHandler MouseOut;

		public event EventHandler MouseUp;

		/// <summary>
		/// Now is not work
		/// </summary>
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
						this.OnMouseClick(null);
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

		public void OnMouseClick(EventArgs e)
		{
			this.MouseClick?.Invoke(this, e);
		}

		public void OnMouseDown(EventArgs e)
		{
			this.MouseDown?.Invoke(this, e);
		}

		public void OnMouseOut(EventArgs e)
		{
			this.MouseOut?.Invoke(this, e);
		}

		public void OnMouseUp(EventArgs e)
		{
			this.MouseUp?.Invoke(this, e);
		}
	}
}