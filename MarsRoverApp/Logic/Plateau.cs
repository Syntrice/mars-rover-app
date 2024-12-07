using MarsRoverApp.Input;

namespace MarsRoverApp.Logic
{
    public class Plateau : IRoverObserver
    {
        public PlateauSize Size { get; }

        private Rover?[,] _rovers;

        public Plateau(PlateauSize size)
        {
            Size = size;
            _rovers = new Rover[size.Width, size.Height];
        }

        public Rover? LandRover(RoverPosition roverPosition)
        {
            if (roverPosition.X < 0 || roverPosition.Y < 0 || roverPosition.X >= Size.Width || roverPosition.Y >= Size.Height)
            {
                return null;
            }

            if (_rovers[roverPosition.X, roverPosition.Y] != null)
            {
                return null;
            }
            Rover rover = new Rover(roverPosition.Direction);
            rover.AddObserver(this);
            _rovers[roverPosition.X, roverPosition.Y] = rover;
            return rover;
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

        public void OnRoverMove(Rover rover)
        {
            //TODO: safety checking to make sure this method only exucutes with existing rovers on the Plateau

            RoverPosition? position = GetRoverPosition(rover);

            // Unlikely, but could be null
            if (position == null)
            {
                return;
            }

            // Get new coordinates
            var newCoords = rover.Direction switch
            {
                Direction.North => (x: position.X, y: position.Y + 1),
                Direction.South => (x: position.X, y: position.Y - 1),
                Direction.East => (x: position.X + 1, y: position.Y),
                Direction.West => (x: position.X - 1, y: position.Y),
            };

            // If out of bounds
            if (newCoords.x < 0 || newCoords.y < 0 || newCoords.x >= Size.Width || newCoords.y >= Size.Height)
            {
                return;
            }

            // If collides with another rover
            if (_rovers[newCoords.x, newCoords.y] != null)
            {
                return;
            }

            _rovers[position.X, position.Y] = null;
            _rovers[newCoords.x, newCoords.y] = rover;
        }

        // TODO: Unit Test
        public RoverPosition? GetRoverPosition(Rover rover)
        {
            for (int i = 0; i < _rovers.GetLength(0); i++)
            {
                for (int j = 0; j < _rovers.GetLength(1); j++)
                {
                    if (rover == _rovers[i, j])
                    {
                        return new RoverPosition(i, j, rover.Direction);
                    }
                }
            }

            return null;
        }
    }
}