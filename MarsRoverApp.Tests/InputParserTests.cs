using FluentAssertions;
using MarsRoverApp.Input;

namespace MarsRoverApp.Tests
{
    using Ri = RoverInstruction;

    [TestFixture]
    public class InputParserTests
    {
        [TestCase("5 5", 6, 6)]
        [TestCase("3 5", 4, 6)]
        [TestCase("3 3", 4, 4)]
        [TestCase("10 15", 11, 16)]
        public void ParsePlateauSize_WithValidString_ParsesCorrectly(string input, int expectedWidth, int expectedHeight)
        {
            // Arrange
            PlateauSize? plateauSize;

            // Act
            bool success = InputParser.TryParsePlateauSize(input, out plateauSize);

            // Assert
            success.Should().BeTrue();
            plateauSize.Width.Should().Be(expectedWidth);
            plateauSize.Height.Should().Be(expectedHeight);
        }

        [TestCase("0 0 N", 0, 0, Direction.North)]
        [TestCase("0 0 E", 0, 0, Direction.East)]
        [TestCase("0 0 S", 0, 0, Direction.South)]
        [TestCase("0 0 W", 0, 0, Direction.West)]
        [TestCase("1 5 N", 1, 5, Direction.North)]
        [TestCase("5 1 N", 5, 1, Direction.North)]
        [TestCase("7 10 N", 7, 10, Direction.North)]
        public void ParseRoverPosition_WithValidString_ParsesCorrectly(string input, int expectedX, int expectedY, Direction expectedDirection)
        {
            // Arrange
            RoverPosition? roverPosition;
            var expected = new RoverPosition(expectedX, expectedY, expectedDirection);

            // Act
            bool success = InputParser.TryParseRoverPosition(input, out roverPosition);

            // Assert
            success.Should().BeTrue();
            roverPosition.Should().BeEquivalentTo(expected);
        }

        [TestCase("LRM", new Ri[]
        {
            Ri.TurnLeft,
            Ri.TurnRight,
            Ri.MoveForward
        })]
        [TestCase("MRL", new Ri[]
        {
            Ri.MoveForward,
            Ri.TurnRight,
            Ri.TurnLeft
        })]
        [TestCase("MRLRMLLRR", new Ri[]
        {
            Ri.MoveForward,
            Ri.TurnRight,
            Ri.TurnLeft,
            Ri.TurnRight,
            Ri.MoveForward,
            Ri.TurnLeft,
            Ri.TurnLeft,
            Ri.TurnRight,
            Ri.TurnRight
        })]
        public void ParseRoverInstructions_WithValidString_ParsesCorrectly(string input, Ri[] expected)
        {
            // Arrange
            RoverInstruction[] roverInstructions;

            // Act
            bool success = InputParser.TryParseRoverInstructions(input, out roverInstructions);

            // Assert
            success.Should().BeTrue();
            roverInstructions.Should().BeEquivalentTo(expected);
        }

        [TestCase("")]
        [TestCase("5")]
        //[TestCase("0 0")] minimum size test - unsure what this should be
        [TestCase("-5 5")]
        [TestCase("5 -5")]
        [TestCase("-5 -5")]
        [TestCase("3 a")]
        [TestCase("3 3 3")]
        [TestCase("foobar123")]
        public void ParsePlateauSize_WithInvalidString_ReturnsFalseAndNull(string input)
        {
            // Arrange
            PlateauSize? plateauSize;

            // Act
            bool success = InputParser.TryParsePlateauSize(input, out plateauSize);

            // Assert
            success.Should().BeFalse();
            plateauSize.Should().BeNull();

        }

        [TestCase("")]
        [TestCase("1")]
        [TestCase("1 5")]
        [TestCase("1 5 5")]
        [TestCase("1 5 F")]
        [TestCase("N 5 N")]
        [TestCase("-5 -5 N")]
        [TestCase("foobar123")]
        public void ParseRoverPosition_WithInvalidString_ReturnsFalseAndNull(string input)
        {
            // Arrange
            RoverPosition? roverPosition;

            // Act
            bool success = InputParser.TryParseRoverPosition(input, out roverPosition);

            // Assert
            success.Should().BeFalse();
            roverPosition.Should().BeNull();
        }
        [TestCase("")]
        [TestCase("12325")]
        [TestCase(@"%$&!~")]
        [TestCase("RL!5M")]
        [TestCase("MRLRXMLLRR")]
        public void ParseRoverInstructions_WithInvalidString_ReturnsFalseAndEmptyArray(string input)
        {
            // Arrange
            RoverInstruction[] roverInstructions;

            // Act
            bool success = InputParser.TryParseRoverInstructions(input, out roverInstructions);

            // Assert
            success.Should().BeFalse();
            roverInstructions.Should().BeEmpty();
        }
    }
}