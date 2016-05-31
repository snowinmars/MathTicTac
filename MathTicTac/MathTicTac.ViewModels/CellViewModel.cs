using MathTicTac.Enums;
using MathTicTac.ServiceModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MathTicTac.ViewModels
{
	public class CellViewModel : Button
	{
		public CellViewModel(State state,
					CoordServiceModel position,
					int width,
					int height,
					string buttonText = "") : base(new Vector2(position.X, position.Y),
										width,
										height,
										buttonText)
		{
			this.State = state;
		}

		public State State { get; set; }

		public override void Update()
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

		#region equals

		public static bool operator !=(CellViewModel lhs, CellViewModel rhs)
		    => !(lhs == rhs);

		public static bool operator ==(CellViewModel lhs, CellViewModel rhs)
		{
			object olhs = (object)lhs;
			object orhs = (object)rhs;

			if (olhs == null && orhs == null)
			{
				return true;
			}

			if (olhs == null ^ orhs == null)
			{
				return false;
			}

			return lhs.State == rhs.State;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			CellViewModel p = obj as CellViewModel;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(CellViewModel obj)
		{
			if ((object)obj == null)
			{
				return false;
			}

			return this == obj;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.State.GetHashCode();
		}

		#endregion equals
	}
}