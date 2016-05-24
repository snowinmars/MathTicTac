namespace MathTicTac.PL.Monogame.DTO
{
	using Entities;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	internal class CellDTO : Button
	{
		public State State { get; set; }

		public CellDTO(State state, Vector2 position, int width, int height, string buttonText = "") : base(position, width, height, buttonText)
		{
			this.State = state;
		}

		public override void Draw(SpriteBatch bath)
		{
			switch (this.State)
			{
				case State.None:
					bath.Draw(MonogameStock.noneCellTexture, base.rectangle, Color.White);
					break;

				case State.Client:
					textures = MonogameStock.cellsCrossTextures;
					bath.Draw(textures[currentVisibleState], base.rectangle, Color.White);
					break;

				case State.Enemy:
					textures = MonogameStock.cellsZeroTextures;
					bath.Draw(textures[currentVisibleState], base.rectangle, Color.White);
					break;

				default:
					break;
			}
		}

		#region equals

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			CellDTO p = obj as CellDTO;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(CellDTO obj)
		{
			if ((object)obj == null)
			{
				return false;
			}

			return this == obj;
		}

		public static bool operator !=(CellDTO lhs, CellDTO rhs)
		    => !(lhs == rhs);

		public static bool operator ==(CellDTO lhs, CellDTO rhs)
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

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.State.GetHashCode();
		}

		#endregion equals
	}
}