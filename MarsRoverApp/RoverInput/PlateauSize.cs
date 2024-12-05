using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApp.RoverInput
{
    public struct PlateauSize(int width, int height)
    {
        public int Height { get; } = height;
        public int Width { get; } = width;
    }
}
