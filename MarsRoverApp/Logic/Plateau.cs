using MarsRoverApp.Input;

namespace MarsRoverApp.Logic
{
    public class Plateau
    {
        public PlateauSize Size { get; }

        private Rover[,] _rovers;

        public Plateau(PlateauSize size)
        {
            Size = size;
            _rovers = new Rover[size.Width, size.Height];
        }

        public void LandRover(RoverPosition roverPosition)
        {
            if (roverPosition.x < 0 ||  roverPosition.y < 0 || roverPosition.x >= Size.Width || roverPosition.y >= Size.Height)
            {
                return;
            }

            if (_rovers[roverPosition.x, roverPosition.y] != null)
            {
                return;
            }

            _rovers[roverPosition.x, roverPosition.y] = new Rover(roverPosition.direction);
            
        }

        public Rover? GetRoverAtPos(int x, int y)
        {
            if (x < 0 || x >= Size.Width)
            {
                throw new ArgumentOutOfRangeException("x");
            }

            if (y < 0 || y >= Size.Height)
            { 
                throw new ArgumentOutOfRangeException("y"); 
            }

            return _rovers[x, y];
        }
    }
}
