namespace MarsRoverApp
{
    public interface IApplication
    {
        public bool IsRunning { get; }

        public void Run();
        public void Stop();

    }
}
