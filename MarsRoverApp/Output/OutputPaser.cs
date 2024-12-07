using MarsRoverApp.Input;

namespace MarsRoverApp.Output
{
    public static class OutputPaser
    {
        public static bool TryParseRoverPosition(RoverPosition? position, out string output)
        {
            output = "";
            if (position == null)
            {
                return false;
            }

            string[] parts = new string[3];
            parts[0] = position.X.ToString();
            parts[1] = position.Y.ToString();

            parts[2] = position.Direction switch
            {
                Direction.North => "N",
                Direction.East => "E",
                Direction.South => "S",
                Direction.West => "W",
            };

            output = string.Join(' ', parts);
            return true;
        }

    }
}
