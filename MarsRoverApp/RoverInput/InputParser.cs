namespace MarsRoverApp.RoverInput
{
    public static class InputParser
    {
        public static PlateauSize ParsePlateauSize(string input)
        {
            return new PlateauSize(0, 0);
        }

        public static RoverPosition ParseRoverPosition(string input)
        {
            return new RoverPosition(0, 0, Direction.North);
        }

        public static RoverInstruction[] ParseRoverInstructions(string input)
        {
            return new RoverInstruction[0];
        }
    }
}