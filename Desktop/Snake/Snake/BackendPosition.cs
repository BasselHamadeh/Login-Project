using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class BackendPosition
    {
        public int Row { get; }
        public int Column { get; }

        public BackendPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public BackendPosition Translate(BackendDirection direction)
        {
            return new BackendPosition(Row + direction.Row, Column + direction.Column);
        }

        public override bool Equals(object obj)
        {
            return obj is BackendPosition position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode()
        {
            int hashCode = 240067226;
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            hashCode = hashCode * -1521134295 + Column.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(BackendPosition left, BackendPosition right)
        {
            return EqualityComparer<BackendPosition>.Default.Equals(left, right);
        }

        public static bool operator !=(BackendPosition left, BackendPosition right)
        {
            return !(left == right);
        }
    }
}
