using Spectre.Console;

namespace MarsRoverApp.ConsoleGUI
{
    public abstract class State : IState
    {
        protected Application _application;

        public State(Application application)
        {
            _application = application;
        }

        public void Clear()
        {
            AnsiConsole.Clear();
            Rule rule = new Rule("[orangered1]Mars Rover Application[/]");
            rule.Justification = Justify.Left;
            AnsiConsole.Write(rule);
            AnsiConsole.WriteLine();
        }

        public abstract void Run();
    }
}
