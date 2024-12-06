using MarsRoverApp.Input;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace MarsRoverApp.ConsoleGUI
{
    public class LandRoverState(Application application) : State(application)
    {
        public override void Run()
        {
            PrintPlateauState();
            var selection = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Select a command:")
                .AddChoices(new[]
                {
                    "Land Rover",
                    "Exit"
                } 
                ));

            switch (selection)
            {
                case "Land Rover":
                    LandRover();
                    break;
                case "Exit":
                    _application.Stop();
                    break;
            }
        }

        private void PrintPlateauState()
        {
            AnsiConsole.MarkupLine("[bold]Plateau Map[/]");
            string plateauState = OutputPaser.ParsePlateau(_application.Plateau);
            var panel = new Panel(plateauState);
            panel.Border = BoxBorder.Heavy;
            AnsiConsole.Write(panel);
            AnsiConsole.WriteLine();
        }

        private void LandRover()
        {
            Clear();
            PrintPlateauState();

            var roverX = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter rover X coordinate:")
                    .Validate(x => x >= 0 && x < _application.Plateau.Size.Width)
                    .ValidationErrorMessage($"[red]Invalid input: must be a positive integer value greater than or equal to 0 and less than {_application.Plateau.Size.Width}.[/]")
            );

            var roverY = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter rover Y coordinate:")
                    .Validate(y => y >= 0 && y < _application.Plateau.Size.Height)
                    .ValidationErrorMessage($"[red]Invalid input: must be a positive integer value greater than or equal to 0 and less than {_application.Plateau.Size.Height}.[/]")
            );

            var roverDirectionInput = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Select rover direction:")
                .AddChoices(new[]
                {
                    "North",
                    "East",
                    "South",
                    "West"
                }
                ));

            var roverDirection = roverDirectionInput switch
            {
                "North" => Direction.North,
                "East" => Direction.East,
                "South" => Direction.South,
                "West" => Direction.West
            };

            var instructionsString = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter a string of rover instructions [[R, L, M]]:")
                    .Validate(s => Regex.Match(s, @"^[RLMrlm]*$").Success)
                    .AllowEmpty()
                    .ValidationErrorMessage("[red]Invalid input: instructions must only contain the following characters: R, L, M.[/]")
            );

            RoverPosition position = new RoverPosition(roverX, roverY, roverDirection);

            RoverInstruction[] instructions;

            if (!string.IsNullOrEmpty(instructionsString))
            {
                instructionsString = instructionsString.ToUpper();
                instructions = InputParser.ParseRoverInstructions(instructionsString);
            }
            else
            {
                instructions = new RoverInstruction[0];
            }

            _application.LandAndInstructRover(position, instructions);


            AnsiConsole.MarkupLine($"[green]Attempted to land rover and instruct according to the specified inputs![/]");
            AnsiConsole.MarkupLine("[bold]Press any key to continue...[/]");
            Console.ReadKey();
        }
    }
}
