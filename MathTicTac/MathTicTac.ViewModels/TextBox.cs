using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MathTicTac.ViewModels
{
	public class TextBox : Button
	{
		public TextBox(Vector2 position, int width, int height, string buttonText = "") : base(position, width, height, buttonText)
		{
			this.buttonText = new List<char>(64);
		}

		//new StringBuilder realisation is slow for ToString()
		public new List<char> buttonText;

		public bool IsFocused { get; set; }

		public override void Draw(SpriteBatch bath)
		{
			bath.Draw(this.textures[currentVisibleState], this.rectangle, Color.White);
			bath.DrawString(Font, new string(buttonText.ToArray()), Position, Color.Black);
		}

		public override void Update()
		{

			MouseState mouseState = Mouse.GetState();

			if (this.rectangle.Contains(mouseState.X, mouseState.Y))
			{
				if (mouseState.LeftButton == ButtonState.Pressed)
				{
					this.IsFocused = true;
				}
			}
			else
			{
				if (mouseState.LeftButton == ButtonState.Pressed)
				{
					this.IsFocused = false;
				}
			}

			if (this.IsFocused)
			{
				KeyboardState state = Keyboard.GetState();

				Keys[] keys = state.GetPressedKeys();

				foreach (var key in keys)
				{
					Debug.WriteLine($"Key {key} is down: {state.IsKeyDown(key)}, up: {state.IsKeyUp(key)}");

					if (!state.IsKeyUp(key))
					{
						// writing symbols
						if ((key >= Keys.A && key <= Keys.Z) ||
							key == Keys.Space)
						{
							char c = (char)key;
							this.buttonText.Add(c);
						}

						//
						if ((key == Keys.Back) &&
							(this.buttonText.Count > 0))
						{
							this.buttonText.RemoveAt(this.buttonText.Count - 1);
						}
					}
				}
			}
		}
	}
}