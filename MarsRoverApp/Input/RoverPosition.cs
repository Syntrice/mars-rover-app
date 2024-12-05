using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApp.Input
{
    public struct RoverPosition(int x, int y, Direction direction)
    {
        public int x { get; } = x;
        public int y { get; } = y;
        public Direction direction { get; } = direction;
    }
}
