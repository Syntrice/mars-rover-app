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
        [TestCase(5,5, Direction.West)]
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

        private Plateau GetDefaultPlateau()
        {
            return new Plateau(new PlateauSize(5, 5));
        }


    }
}
