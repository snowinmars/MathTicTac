namespace MathTicTac.Entities
{
    public class Cell
    {
        public State State { get; set; }
        public bool IsFocus { get; set; }

        public Cell() : this(State.None, false)
        {
        }

        public Cell(State state, bool isFocus)
        {
            this.State = state;
            this.IsFocus = isFocus;
        }

        #region equals

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Cell p = obj as Cell;
            if ((object)p == null)
            {
                return false;
            }

            return this.Equals(p);
        }

        public bool Equals(Cell obj)
        {
            if ((object)obj == null)
            {
                return false;
            }

            return this == obj;
        }

        public static bool operator !=(Cell lhs, Cell rhs)
            => !(lhs == rhs);

        public static bool operator ==(Cell lhs, Cell rhs)
        {
            if ((lhs == null) && (rhs == null))
            {
                return true;
            }

            if ((lhs == null) ^ (rhs == null))
            {
                return false;
            }

            return lhs.IsFocus == rhs.IsFocus &&
                lhs.State == rhs.State;
        }

        public override int GetHashCode()
        {
            return this.IsFocus.GetHashCode() ^ this.State.GetHashCode();
        }

        #endregion equals
    }
}