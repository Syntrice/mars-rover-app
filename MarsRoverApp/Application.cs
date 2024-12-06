namespace MarsRoverApp
{
    public class Application : IApplication
    {
        public IState? State { get; set; }

        public bool IsRunning { get; private set; } 

        public void Run(IState startingState)
        {
            IsRunning = true;
            State = startingState;
            while (true)
            {
                State.Run();
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
