using FluentAssertions;
using MarsRoverApp.Input;
using MarsRoverApp.Logic;

namespace MarsRoverApp.Tests
{
    [TestFixture]
    public class PlateauTests
    {
        [TestCase(0, 0, Direction.North)]
        [TestCase(1, 3, Direction.East)]
        [TestCase(3, 1, Direction.South)]
        [TestCase(4, 4, Direction.West)]
        public void GetRoverAtPos_ExistingRover_ReturnExpectedRover(int x, int y, Direction direction)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();
            Rover expectedRover = new Rover(direction);
            plateau.TryRoverLanding(expectedRover, x, y);

            // Act
            Rover? actualRover = plateau.GetRoverAtPos(x, y);

            // Assert
            actualRover.Should().Be(expectedRover);
        }

        [TestCase(5, 5)]
        [TestCase(-5, -5)]
        [TestCase(3, -3)]
        [TestCase(-3, 3)]
        [TestCase(50, 50)]
        public void GetRoverAtPos_OutOfBounds_ShouldThrowArgumentOutOfRangeException(int x, int y)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();

            // Act
            Action action = () =>
            {
                plateau.GetRoverAtPos(x, y);
            };

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestCase(3, 3, Direction.North, 3, 4)]
        [TestCase(3, 3, Direction.East, 4, 3)]
        [TestCase(3, 3, Direction.South, 3, 2)]
        [TestCase(3, 3, Direction.West, 2, 3)]
        public void OnRoverMove_WhenRoverMovesToEmptyCoordinate_RoverPositionUpdates(
            int startX,
            int startY,
            Direction direction,
            int finalX,
            int finalY)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();
            Rover rover = new Rover(direction);
            plateau.TryRoverLanding(rover, startX, startY);

            // Act
            rover.Instruct(RoverInstruction.MoveForward);

            // Assert
            plateau.GetRoverAtPos(startX, startY).Should().BeNull();
            plateau.GetRoverAtPos(finalX, finalY).Should().Be(rover);
        }

        [TestCase(4, 4, Direction.North)]
        [TestCase(4, 4, Direction.East)]
        [TestCase(0, 0, Direction.South)]
        [TestCase(0, 0, Direction.West)]
        public void OnRoverMove_WhenRoverMovesOutOfBounds_RoverPositionStaysSame(int startX, int startY, Direction direction)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();
            Rover rover = new Rover(direction);
            plateau.TryRoverLanding(rover, startX, startY);

            // Act
            rover.Instruct(RoverInstruction.MoveForward);

            // Assert
            plateau.GetRoverAtPos(startY, startY).Should().Be(rover);
        }

        [TestCase(3, 3, Direction.North, 3, 4)]
        [TestCase(3, 3, Direction.East, 4, 3)]
        [TestCase(3, 3, Direction.South, 3, 2)]
        [TestCase(3, 3, Direction.West, 2, 3)]
        public void OnRoverMove_WhenRoverMovesToNonEmptyCoordinate_RoverPositionStaysSame(
            int startX,
            int startY,
            Direction direction,
            int secondRoverX,
            int secondRoverY)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();

            Rover rover1 = new Rover(direction);
            Rover rover2 = new Rover(Direction.North);

            plateau.TryRoverLanding(rover1, startX, startY);
            plateau.TryRoverLanding(rover2 , secondRoverX, secondRoverY);

            // Act
            rover1.Instruct(RoverInstruction.MoveForward);

            // Assert
            plateau.GetRoverAtPos(startX, startY).Should().Be(rover1);
            plateau.GetRoverAtPos(secondRoverX, secondRoverY).Should().Be(rover2);
        }

        [TestCase(5, 5)]
        [TestCase(-5, -5)]
        [TestCase(3, -3)]
        [TestCase(-3, 3)]
        [TestCase(50, 50)]
        public void TryLandRover_OutOfBounds_ShouldReturnFalse(int x, int y)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();

            // Act
            Rover rover = new Rover(Direction.North);
            bool success = plateau.TryRoverLanding(rover, x, y);

            // Assert
            success.Should().BeFalse();
        }

        [TestCase(0, 0)]
        [TestCase(1, 3)]
        [TestCase(3, 1)]
        [TestCase(4, 4)]
        public void LandRover_RoverExistsAtLocation_ShouldNotOverwriteRoverReturnsFalse(int x, int y)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();
            Rover expectedRover = new Rover(Direction.North);
            plateau.TryRoverLanding(expectedRover, x, y);

            // Act
            Rover rover2 = new Rover(Direction.North);
            bool success = plateau.TryRoverLanding(rover2, x, y);

            // Assert
            success.Should().BeFalse();
            plateau.GetRoverAtPos(x, y).Should().Be(expectedRover);
        }

        private Plateau GetDefaultPlateau()
        {
            return new Plateau(new PlateauSize(5, 5));
        }
    }
}