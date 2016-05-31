using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.ViewModels
{
	public class TextBox : Button
	{
		public TextBox(Vector2 position, int width, int height, string buttonText = "") : base(position, width, height, buttonText)
		{
		}

		public bool IsFocused { get; set; }

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

				foreach (var item in keys)
				{
					this.buttonText += item;

					if (item == Keys.Back)
					{
						// TODO
					}
					break; // TODO
				}
			}
		}
	}
}
