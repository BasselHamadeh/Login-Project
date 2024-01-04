using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class BackendDirection
    {
        public readonly static BackendDirection Left = new BackendDirection(0, -1);
        public readonly static BackendDirection Right = new BackendDirection(0, 1);
        public readonly static BackendDirection Up = new BackendDirection(-1, 0);
        public readonly static BackendDirection Down = new BackendDirection(1, 0);

        public int Row { get; }
        public int Column { get; }

        public BackendDirection(int row, int column)
        {
            Row = row; 
            Column = column;
        }

        public BackendDirection Opposite()
        {
            return new BackendDirection(-Row, -Column);
        }

        public override bool Equals(object obj)
        {
            return obj is BackendDirection direction &&
                   Row == direction.Row &&
                   Column == direction.Column;
        }

        public override int GetHashCode()
        {
            int hashCode = 240067226;
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            hashCode = hashCode * -1521134295 + Column.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(BackendDirection left, BackendDirection right)
        {
            return EqualityComparer<BackendDirection>.Default.Equals(left, right);
        }

        public static bool operator !=(BackendDirection left, BackendDirection right)
        {
            return !(left == right);
        }
    }
}
