namespace MarsRoverApp
{
    public class Application : IApplication
    {

        public bool IsRunning { get; private set; } 

        public void Run()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
