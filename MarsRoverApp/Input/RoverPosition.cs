using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApp.Input
{
    public struct RoverPosition(int x, int y, Direction direction)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
        public Direction Direction { get; } = direction;
    }
}
