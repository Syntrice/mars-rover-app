using FluentAssertions;
using MarsRoverApp.Input;
using MarsRoverApp.Logic;

namespace MarsRoverApp.Tests
{
    [TestFixture]
    public class RoverTests
    {
        [TestCase(Direction.North, RoverInstruction.TurnRight, Direction.East)]
        [TestCase(Direction.East, RoverInstruction.TurnRight, Direction.South)]
        [TestCase(Direction.South, RoverInstruction.TurnRight, Direction.West)]
        [TestCase(Direction.West, RoverInstruction.TurnRight, Direction.North)]
        public void Instruct_WithTurnRight_SetsDirectionCorrectly(Direction startingDirection, RoverInstruction instruction, Direction finalDirection)
        {
            // Arrange
            Rover rover = new Rover(startingDirection);

            // Act
            rover.Instruct(instruction);

            // Assert
            rover.Direction.Should().Be(finalDirection);
        }

        [TestCase(Direction.North, RoverInstruction.TurnLeft, Direction.West)]
        [TestCase(Direction.East, RoverInstruction.TurnLeft, Direction.North)]
        [TestCase(Direction.South, RoverInstruction.TurnLeft, Direction.East)]
        [TestCase(Direction.West, RoverInstruction.TurnLeft, Direction.South)]
        public void Instruct_WithTurnLeft_SetsDirectionCorrectly(Direction startingDirection, RoverInstruction instruction, Direction finalDirection)
        {
            // Arrange
            Rover rover = new Rover(startingDirection);

            // Act
            rover.Instruct(instruction);

            // Assert
            rover.Direction.Should().Be(finalDirection);
        }
    }
}
