using MarsRoverApp.Input;

namespace MarsRoverApp.Logic
{
    public class Rover
    {
        public Direction Direction { get; private set; }

        public Rover(Direction initialDirection)
        {
            Direction = initialDirection;
        }

        public void Instruct(RoverInstruction instruction)
        {

        }
    }
}
