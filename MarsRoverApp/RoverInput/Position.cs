using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApp.RoverInput
{
    public struct Position(int x, int y, Direction direction)
    {
        public int x { get; } = x;
        public int y { get; } = y;
        public Direction direction { get; } = direction;
    }
}
