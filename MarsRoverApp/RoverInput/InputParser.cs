namespace MarsRoverApp.RoverInput
{
    public static class InputParser
    {
        public static PlateauSize ParsePlateauSizeString(string input)
        {
            return new PlateauSize(0, 0);
        }

        public static RoverPosition ParsePositionString(string input)
        {
            return new RoverPosition(0, 0, Direction.North);
        }

        public static RoverInstruction ParseRoverInstructionString(string input)
        {
            return RoverInstruction.MoveForward;
        }
    }
}