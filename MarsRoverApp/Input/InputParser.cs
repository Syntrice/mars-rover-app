using System.Text.RegularExpressions;

namespace MarsRoverApp.Input
{
    public static class InputParser
    {
        public static bool TryParsePlateauSize(string input, out PlateauSize? plateauSize)
        {
            plateauSize = null;

            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            string[] coords = input.Split(' ');

            if (coords.Length != 2)
            {
                return false;
            }

            if (!int.TryParse(coords[0], out int x) || !int.TryParse(coords[1], out int y))
            {
                return false;
            }

            if (x  < 0 || y < 0)
            {
                return false;
            }

            int width = x + 1;
            int height = y + 1;

            plateauSize = new PlateauSize(width, height);
            return true;
        }

        public static bool TryParseRoverPosition(string input, out RoverPosition? roverPosition)
        {

            roverPosition = null;
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            string[] position = input.Split(" ");

            if (position.Length != 3)
            {
                return false;
            }

            if (!int.TryParse(position[0], out int x) || !int.TryParse(position[1], out int y))
            {
                return false;
            }

            if (x < 0 || y < 0)
            {
                return false;
            }

            if (!Regex.IsMatch(position[2].ToString(), @"^[NESWnesw]+$"))
            {
                return false;
            }

            Direction direction = position[2].ToUpper() switch
            {
                "N" => Direction.North,
                "E" => Direction.East,
                "S" => Direction.South,
                "W" => Direction.West
            };

            roverPosition = new RoverPosition(x, y, direction);
            return true;
        }

        public static bool TryParseRoverInstructions(string input, out RoverInstruction[] roverInstructions)
        {
            roverInstructions = new RoverInstruction[0];

            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if (!Regex.IsMatch(input, @"^[LRMlrm]+$"))
            {
                return false;
            }

            var instructions = input.ToUpper().Select(c => c switch
            {
                'L' => RoverInstruction.TurnLeft,
                'R' => RoverInstruction.TurnRight,
                'M' => RoverInstruction.MoveForward
            }
            );

            roverInstructions = instructions.ToArray();
            return true;
        }
    }
}