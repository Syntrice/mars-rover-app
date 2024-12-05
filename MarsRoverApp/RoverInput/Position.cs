using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApp.RoverInput
{
    public struct Position
    {
        public int x { get; }
        public int y { get; }
        public Direction direction { get; }

        public Position(int x, int y, Direction direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
        }
    }
}
