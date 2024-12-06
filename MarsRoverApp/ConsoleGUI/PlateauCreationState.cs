using MarsRoverApp.Input;
using MarsRoverApp.Logic;
using Spectre.Console;

namespace MarsRoverApp.ConsoleGUI
{
    internal class PlateauCreationState(Application application) : State(application)
    {
        public override void Run()
        {
            var plateauWidth = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter plateau Width:")
                    .Validate(width => width > 0)
                    .ValidationErrorMessage("[red]Invalid input: must be a positive integer value.[/]")
            );

            var plateauHeight = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter plateau Height:")
                    .Validate(height => height > 0)
                    .ValidationErrorMessage("[red]Invalid input: must be a positive integer value.[/]")
            );

            PlateauSize plateauSize = new PlateauSize(plateauWidth, plateauHeight);
            _application.CreatePlateau(plateauSize);

            AnsiConsole.MarkupLine($"[green]Created plateau with width {plateauWidth} and height {plateauHeight}.[/]");
            AnsiConsole.MarkupLine("[bold]Press any key to continue...[/]");
            _application.State = new LandRoverState(_application);

        }
    }
}
