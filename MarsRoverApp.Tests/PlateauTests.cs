using FluentAssertions;
using MarsRoverApp.Input;
using MarsRoverApp.Logic;

namespace MarsRoverApp.Tests
{
    [TestFixture]
    public class PlateauTests
    {
        [TestCase(0,0, Direction.North)]
        [TestCase(1,3, Direction.East)]
        [TestCase(3,1, Direction.South)]
        [TestCase(4,4, Direction.West)]
        public void GetRoverAtPos_ExistingRover_ReturnExpectedRover(int x, int y, Direction direction)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();
            plateau.LandRover(new RoverPosition(x, y, direction));

            // Act
            Rover? rover = plateau.GetRoverAtPos(x, y);

            // Assert
            rover.Should().NotBeNull();
            rover.Direction.Should().Be(direction);
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

        [TestCase(3, 3, Direction.North, 3,4)]
        [TestCase(3, 3, Direction.East,4,3)]
        [TestCase(3, 3, Direction.South,3,2)]
        [TestCase(3, 3, Direction.West,2,3)]
        public void OnRoverMove_WhenRoverMovesToEmptyCoordinate_RoverPositionUpdates(
            int startX,
            int startY,
            Direction direction,
            int finalX,
            int finalY)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();
            plateau.LandRover(new RoverPosition(startX,startY, direction));
            Rover rover = plateau.GetRoverAtPos(startX, startY);

            // Act
            rover.Instruct(RoverInstruction.MoveForward);

            // Assert
            plateau.GetRoverAtPos(startX, startY).Should().BeNull();
            plateau.GetRoverAtPos(finalX, finalY).Should().Be(rover);
        }

        [TestCase(4,4,Direction.North)]
        [TestCase(4,4,Direction.East)]
        [TestCase(0,0,Direction.South)]
        [TestCase(0,0,Direction.West)]
        public void OnRoverMove_WhenRoverMovesOutOfBounds_RoverPositionStaysSame(int startX, int startY, Direction direction)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();
            plateau.LandRover(new RoverPosition(startX, startY, direction));
            Rover rover = plateau.GetRoverAtPos(startX, startY);

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

            plateau.LandRover(new RoverPosition(startX, startY, direction));
            plateau.LandRover(new RoverPosition(secondRoverX, secondRoverY, Direction.North));

            Rover rover = plateau.GetRoverAtPos(startX, startY);
            Rover secondRover = plateau.GetRoverAtPos(secondRoverX, secondRoverY);

            // Act
            rover.Instruct(RoverInstruction.MoveForward);

            // Assert
            plateau.GetRoverAtPos(startX,startY).Should().Be(rover);
            plateau.GetRoverAtPos(secondRoverX, secondRoverY).Should().Be(secondRover); 

        }


        [TestCase(5, 5)]
        [TestCase(-5, -5)]
        [TestCase(3, -3)]
        [TestCase(-3, 3)]
        [TestCase(50, 50)]
        public void LandRover_OutOfBounds_ShouldNotThrow(int x, int y)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();

            // Act
            Action action = () =>
            {
                plateau.LandRover(new RoverPosition(x, y, Direction.North));
            };

            // Assert
            action.Should().NotThrow();
        }

        [TestCase(0, 0)]
        [TestCase(1, 3)]
        [TestCase(3, 1)]
        [TestCase(4, 4)]
        public void LandRover_RoverExistsAtLocation_ShouldNotOverwriteRover(int x, int y)
        {
            // Arrange
            Plateau plateau = GetDefaultPlateau();
            plateau.LandRover(new RoverPosition(x,y, Direction.North));
            Rover? expectedRover = plateau.GetRoverAtPos(x, y);

            // Act
            plateau.LandRover(new RoverPosition(x, y, Direction.North));

            // Assert
            plateau.GetRoverAtPos(x,y).Should().Be(expectedRover);
        }

        private Plateau GetDefaultPlateau()
        {
            return new Plateau(new PlateauSize(5, 5));
        }


    }
}
