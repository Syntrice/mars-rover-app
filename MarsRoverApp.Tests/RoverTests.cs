using FluentAssertions;
using MarsRoverApp.Input;
using MarsRoverApp.Logic;
using Moq;

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

        [Test]
        public void Instruct_WithMoveForward_NotifiesObservers()
        {
            // Arrange
            Mock<IRoverObserver> roverObserverMock = new Mock<IRoverObserver>();
            Rover rover = new Rover(Direction.North);
            rover.AddObserver(roverObserverMock.Object);

            // Act
            rover.Instruct(RoverInstruction.MoveForward);

            // Assert
            roverObserverMock.Verify(observer => observer.OnRoverMove(rover), Times.Once());
        }

        [TestCase(new RoverInstruction[]
        {
            RoverInstruction.TurnLeft,
            RoverInstruction.MoveForward,
            RoverInstruction.TurnRight,
            RoverInstruction.MoveForward,
            RoverInstruction.MoveForward,
            RoverInstruction.TurnLeft,
            RoverInstruction.MoveForward,
            RoverInstruction.TurnRight,
            RoverInstruction.TurnRight,
            RoverInstruction.TurnRight
        }, Direction.South, 4)]
        public void Instruct_MultipleInstructions_AllPassedCorrectly(RoverInstruction[] instructions, Direction finalDirection, int moveCount)
        {
            // Arrange
            Mock<IRoverObserver> roverObserverMock = new Mock<IRoverObserver>();
            Rover rover = new Rover(Direction.North);
            rover.AddObserver(roverObserverMock.Object);

            rover.Instruct(instructions);

            // Assert
            roverObserverMock.Verify(observer => observer.OnRoverMove(rover), Times.Exactly(4));
            rover.Direction.Should().Be(finalDirection);
        }
    }
}
