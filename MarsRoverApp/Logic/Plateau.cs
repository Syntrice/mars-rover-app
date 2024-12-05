using MarsRoverApp.Input;

namespace MarsRoverApp.Logic
{
    public class Plateau
    {
        public PlateauSize Size { get; }

        public Plateau(PlateauSize size)
        {
            Size = size;
        }

        public void LandRover(RoverPosition roverPosition)
        {
            // TODO
        }

        public Rover? GetRoverAtPos(int x, int y)
        {
            // TODO
            return null;
        }
    }
}
