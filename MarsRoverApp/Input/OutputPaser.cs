namespace MarsRoverApp.Input
{
    public static class OutputPaser
    {
        public static string ParseRoverPosition(RoverPosition position)
        {
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

            return string.Join(' ', parts);
        }

    }
}
