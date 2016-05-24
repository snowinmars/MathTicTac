namespace MathTicTac.Entities
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;
	using System;
	using System.Collections.Generic;

	

	public class Button
	{
		private readonly string _buttonText;
		private readonly Vector2 _position;
		private readonly Rectangle _rectangle;
		private Dictionary<VisibleState, Texture2D> _textures = new Dictionary<VisibleState, Texture2D>();
		private VisibleState _currentState = VisibleState.Normal;
		private VisibleState _previousState = VisibleState.Normal;
		private MouseState currentMouseState;
		private MouseState previousMouseState;

		public Button(Vector2 position, int width, int height, string buttonText = "")
		{
			_position = position;
			_buttonText = buttonText;
			_rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
		}

		public void SetTextures(Dictionary<VisibleState, Texture2D> dict)
		{
			this._textures = dict; // due to I wanna all cells have same copy of textures.
		}

		public event EventHandler MouseClick;

		public event EventHandler MouseDown;

		public event EventHandler MouseOut;

		public event EventHandler MouseUp;

		public SpriteFont Font { get; set; }

		public Dictionary<VisibleState, Texture2D> Textures
		{
			get { return _textures; }
		}

		public void Draw(SpriteBatch bath)
		{
			 bath.Draw(_textures[_currentState], _rectangle, Color.White);
			//bath.DrawString(Font, _buttonText, _position, Microsoft.Xna.Framework.Color.Black);
		}

		public void Update()
		{
			_previousState = _currentState;

			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			if (_rectangle.Contains(currentMouseState.X, currentMouseState.Y))
			{
				if (currentMouseState.LeftButton == ButtonState.Pressed)
				{
					if (previousMouseState.LeftButton == ButtonState.Released)
					{
						OnMouseDown(EventArgs.Empty);
						_currentState = VisibleState.Pressed;
					}
					else if (_currentState != VisibleState.Pressed)
					{
						_currentState = VisibleState.Hover;
					}
				}
				else
				{
					if (_previousState == VisibleState.Pressed)
					{
						OnMouseClick(EventArgs.Empty);
					}
					_currentState = VisibleState.Hover;
				}
			}
			else
			{
				if (_previousState == VisibleState.Hover || _previousState == VisibleState.Pressed)
				{
					OnMouseOut(EventArgs.Empty);
				}
				_currentState = VisibleState.Normal;
			}

			if (currentMouseState.LeftButton == ButtonState.Released
			    && _previousState == VisibleState.Pressed)
			{
				OnMouseUp(EventArgs.Empty);
			}
		}

		private void OnMouseClick(EventArgs e)
		{
			MouseClick?.Invoke(this, e);
		}

		private void OnMouseDown(EventArgs e)
		{
			MouseDown?.Invoke(this, e);
		}

		private void OnMouseOut(EventArgs e)
		{
			MouseOut?.Invoke(this, e);
		}

		private void OnMouseUp(EventArgs e)
		{
			MouseUp?.Invoke(this, e);
		}
	}
}