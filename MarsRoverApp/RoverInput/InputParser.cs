using System.Text.RegularExpressions;

namespace MarsRoverApp.RoverInput
{
    public static class InputParser
    {
        public static PlateauSize ParsePlateauSize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Expected: 2 integers separated by a space. Recieved: empty string.");
            }

            string[] coords = input.Split(' ');

            if (coords.Length != 2)
            {
                throw new ArgumentException("Expected: 2 positive integers separated by a space. Recieved: wrong number of values");
            }

            if (!int.TryParse(coords[0], out int x) || !int.TryParse(coords[1], out int y))
            {
                throw new ArgumentException("Expected: 2 positve integers separated by a space. Recieved: one or more invalid characters");
            }

            if (x  < 0 || y < 0)
            {
                throw new ArgumentException("Expected: 2 positive integers separated by a space. Recieved: one or more negative values");
            }

            int width = x + 1;
            int height = y + 1;

            return new PlateauSize(width, height);
        }

        public static RoverPosition ParseRoverPosition(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Expected: string in format 'int int char'. Recieved: empty string.");
            }

            string[] position = input.Split(" ");

            if (position.Length != 3)
            {
                throw new ArgumentException("Expected: string containing three values in format 'int int Direction'. Recieved: wrong number of values.");
            }

            if (!int.TryParse(position[0], out int x) || !int.TryParse(position[1], out int y))
            {
                throw new ArgumentException("Expected: first two values to be positive integers. Recieved: one or more invalid characters.");
            }

            if (x < 0 || y < 0)
            {
                throw new ArgumentException("Expected: first two values to be positive integers. Recieved: one or more negative values");
            }

            Direction direction = position[2] switch
            {
                "N" => Direction.North,
                "E" => Direction.East,
                "S" => Direction.South,
                "W" => Direction.West,
                _ => throw new ArgumentException($"Expected: third value to be N, E, S, or W. Recieved: {position[2]}")
            };

            return new RoverPosition(x, y, direction);
        }

        public static RoverInstruction[] ParseRoverInstructions(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Expected: string of rover instructions. Recieved: empty string");
            }

            var roverInstructions = input.Select(c => c switch
            {
                'L' => RoverInstruction.TurnLeft,
                'R' => RoverInstruction.TurnRight,
                'M' => RoverInstruction.MoveForward,
                _ => throw new ArgumentException("Recieved: one or more invalid characters. Valid characters: 'L', 'R', 'M'")
            }
            );

            return roverInstructions.ToArray();
        }
    }
}