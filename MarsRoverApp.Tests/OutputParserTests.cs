using FluentAssertions;
using MarsRoverApp.Input;
using MarsRoverApp.Output;

namespace MarsRoverApp.Tests
{

    [TestFixture]
    public class OutputParserTests
    {
        [TestCase(0, 0, Direction.North, "0 0 N")]
        [TestCase(1, 2, Direction.East, "1 2 E")]
        [TestCase(2, 1, Direction.South, "2 1 S")]
        [TestCase(5, 5, Direction.West, "5 5 W")]
        public void ParseRoverPosition_WithValidInput_ReturnsTrueAndExpectedString(int x, int y, Direction direction, string expected)
        {
            // Arrange
            RoverPosition input = new RoverPosition(x, y, direction);
            string roverPositionString;

            // Act
            bool success = OutputPaser.TryParseRoverPosition(input, out roverPositionString);

            // Assert
            success.Should().BeTrue();
            roverPositionString.Should().Be(expected);
        }


        public void ParseRoverPosition_WithNullInput_ReturnsFalseAndEmptyString(int x, int y, Direction direction, string expected)
        {
            // Arrange
            RoverPosition input = new RoverPosition(x, y, direction);
            string roverPositionString;

            // Act
            bool success = OutputPaser.TryParseRoverPosition(null, out roverPositionString);

            // Assert
            success.Should().BeFalse();
            roverPositionString.Should().Be("");
        }
    }
}
