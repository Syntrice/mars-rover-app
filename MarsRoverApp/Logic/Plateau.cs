using MarsRoverApp.Input;

namespace MarsRoverApp.Logic
{
    public class Plateau : IRoverObserver
    {
        private Rover?[,] _rovers;
        public PlateauSize Size { get; }
        public Plateau(PlateauSize size)
        {
            Size = size;
            _rovers = new Rover[size.Width, size.Height];
        }

        public CollisionType CheckCollisionAtPos(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Size.Width || y >= Size.Height)
            {
                return CollisionType.OutOfBounds;
            }
            else if (_rovers[x, y] != null)
            {
                return CollisionType.Rover;
            }
            else
            {
                return CollisionType.None;
            }
        }

        public Rover? GetRoverAtPos(int x, int y)
        {
            if (x < 0 || x >= Size.Width || y < 0 || y >= Size.Height)
            {
                return null;
            }

            return _rovers[x, y];
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

        public void OnRoverMove(Rover rover)
        {
            //TODO: safety checking to make sure this method only exucutes with existing rovers on the Plateau

            RoverPosition? position = GetRoverPosition(rover);

            if (position == null) return; // Unlikely, but could be null

            // Get new coordinates
            var newCoords = rover.Direction switch
            {
                Direction.North => (x: position.X, y: position.Y + 1),
                Direction.South => (x: position.X, y: position.Y - 1),
                Direction.East => (x: position.X + 1, y: position.Y),
                Direction.West => (x: position.X - 1, y: position.Y),
            };

            // Check for collision
            if (CheckCollisionAtPos(newCoords.x, newCoords.y) == CollisionType.None)
            {
                _rovers[position.X, position.Y] = null;
                _rovers[newCoords.x, newCoords.y] = rover;
            }
        }

        public bool TryRoverLanding(Rover rover, int x, int y)
        {
            if (CheckCollisionAtPos(x, y) == CollisionType.None)
            {
                rover.AddObserver(this);
                _rovers[x, y] = rover;
                return true;
            }
            return false;
        }
    }
}