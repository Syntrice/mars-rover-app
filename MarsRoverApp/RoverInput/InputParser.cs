namespace MarsRoverApp.RoverInput
{
    public static class InputParser
    {
        public static PlateauSize ParsePlateauSize(string input)
        {
            string[] coords = input.Split(' ');
            int width = int.Parse(coords[0]) + 1;
            int height = int.Parse(coords[1]) + 1;

            return new PlateauSize(width, height);
        }

        public static RoverPosition ParseRoverPosition(string input)
        {
            string[] position = input.Split(" ");
            int x = int.Parse(position[0]);
            int y = int.Parse(position[1]);

            Direction direction = position[2] switch
            {
                "N" => Direction.North,
                "E" => Direction.East,
                "S" => Direction.South,
                "W" => Direction.West
            };

            return new RoverPosition(x, y, direction);
        }

        public static RoverInstruction[] ParseRoverInstructions(string input)
        {
            var roverInstructions = input.Select(c => c switch
            {
                'L' => RoverInstruction.TurnLeft,
                'R' => RoverInstruction.TurnRight,
                'M' => RoverInstruction.MoveForward
            }
            );

            return roverInstructions.ToArray();
        }
    }
}