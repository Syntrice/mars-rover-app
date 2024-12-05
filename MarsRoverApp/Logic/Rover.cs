using MarsRoverApp.Input;

namespace MarsRoverApp.Logic
{
    public class Rover
    {
        public Direction Direction { get; private set; }

        private List<IRoverObserver> _observers;

        public Rover(Direction initialDirection)
        {
            Direction = initialDirection;
            _observers = new List<IRoverObserver>();
        }

        public void AddObserver(IRoverObserver observer)
        {
            _observers.Add(observer);
        }

        private void MoveForward()
        {
            _observers.ForEach(observer => observer.OnRoverMove(this));
        }

        public void Instruct(RoverInstruction[] instructions)
        {
            foreach (var instruction in instructions)
            {
                Instruct(instruction);
            }
        }

        public void Instruct(RoverInstruction instruction)
        {
            switch (instruction)
            {
                case RoverInstruction.TurnLeft:
                    Direction = Direction == Direction.North ? Direction.West : Direction - 1;
                    break;
                case RoverInstruction.TurnRight:
                    Direction = Direction == Direction.West ? Direction.North : Direction + 1;
                    break;
                case RoverInstruction.MoveForward:
                    MoveForward();
                    break;
            }
        }
    }
}
