using MarsRoverApp.Input;
using MarsRoverApp.Logic;

namespace MarsRoverApp
{
    public class Application : IApplication
    {
        public IState? State { get; set; }

        public bool IsRunning { get; private set; } 

        public Plateau Plateau { get; set; }

        public void Run(IState startingState)
        {
            IsRunning = true;
            State = startingState;
            while (IsRunning)
            {
                State.Clear();
                State.Run();
            }
        }

        public void CreatePlateau(PlateauSize plateauSize)
        {
            Plateau = new Plateau(plateauSize);
        }

        public void LandAndInstructRover(RoverPosition roverLandingPosition, RoverInstruction[] instructions)
        {
            
            Rover? rover = Plateau.LandRover(roverLandingPosition);

            if (rover == null)
            {
                return;
            }
            rover.Instruct(instructions);
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
