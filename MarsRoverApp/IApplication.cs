namespace MarsRoverApp
{
    public interface IApplication
    {
        public bool IsRunning { get; }

        public void Run(IState startingState);
        public void Stop();

    }
}
