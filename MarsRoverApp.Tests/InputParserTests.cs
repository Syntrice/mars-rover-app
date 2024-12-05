using FluentAssertions;
using MarsRoverApp.RoverInput;

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
            // Act
            var actual = InputParser.ParsePlateauSize(input);

            // Assert
            actual.Width.Should().Be(expectedWidth);
            actual.Height.Should().Be(expectedHeight);
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
            var expected = new RoverPosition(expectedX, expectedY, expectedDirection);

            // Act
            var actual = InputParser.ParseRoverPosition(input);

            // Assert
            actual.Should().BeEquivalentTo(expected);
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
            // Act
            var actual = InputParser.ParseRoverInstructions(input);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase("")]
        [TestCase("5")]
        [TestCase("0 0")]
        [TestCase("-5 5")]
        [TestCase("5 -5")]
        [TestCase("-5 -5")]
        [TestCase("3 a")]
        [TestCase("3 3 3")]
        [TestCase("foobar123")]
        public void ParsePlateauSize_WithInvalidString_ThrowsArgumentException(string input)
        {
            // Act
            Action action = () => 
            { 
                InputParser.ParsePlateauSize(input);
            };

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [TestCase("")]
        [TestCase("1")]
        [TestCase("1 5")]
        [TestCase("1 5 5")]
        [TestCase("1 5 F")]
        [TestCase("N 5 N")]
        [TestCase("-5 -5 N")]
        [TestCase("foobar123")]
        public void ParseRoverPosition_WithInvalidString_ThrowsArgumentException(string input)
        {
            // Act
            Action action = () =>
            {
                InputParser.ParseRoverPosition(input);
            };

            // Assert
            action.Should().Throw<ArgumentException>();
        }
        [TestCase("")]
        [TestCase("12325")]
        [TestCase(@"%$&!~")]
        [TestCase("RL!5M")]
        [TestCase("MRLRXMLLRR")]
        public void ParseRoverInstructions_WithInvalidString_ThrowsArgumentException(string input)
        {
            // Act
            Action action = () =>
            {
                InputParser.ParseRoverInstructions(input);
            };

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}