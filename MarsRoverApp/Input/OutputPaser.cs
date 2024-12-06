using MarsRoverApp.Logic;
using System.Text;

namespace MarsRoverApp.Input
{
    public static class OutputPaser
    {
        public static string ParseRoverPosition(RoverPosition position)
        {
            // TODO: null handling?

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

        public static string ParsePlateau(Plateau plateau)
        {
            StringBuilder builder = new StringBuilder();
            for (int row = 0; row < plateau.Size.Height; row++)
            {
                for (int col = 0; col < plateau.Size.Width; col++)
                {
                    builder.Append(plateau.GetRoverAtPos(col, row) == null ? "." : "R");
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
