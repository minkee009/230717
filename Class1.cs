using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionExtension
{
    public struct Position
    {
        public int x, y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool isCollideOtherPos(Position other)
        {
            return (x == other.x) && (y == other.y);
        }

        public static Position operator -(Position a, Position b)
        {
            return new Position(a.x - b.x, a.y - b.y);
        }
        public static Position operator +(Position a, Position b)
        {
            return new Position(a.x + b.x, a.y + b.y);
        }

        public static Position operator *(Position a, int b) 
        {
            return new Position(a.x * b, a.y * b);
        }
        public static Position operator *(int a, Position b)
        {
            return new Position(b.x * a, b.y * a);
        }

        public static Position operator *(Position a, Position b)
        {
            return new Position(a.x * b.x, a.y * b.y);
        }
    }
}
