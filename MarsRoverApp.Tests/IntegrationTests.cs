using FluentAssertions;
using MarsRoverApp.Input;
using MarsRoverApp.Logic;
using MarsRoverApp.Output;

namespace MarsRoverApp.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void InputWithLogic_ValidInput_CorrectRoverPositions()
        {
            // -- ARRANGE --
            string[] input = [
            "5 5",
            "1 2 N",
            "LMLMLMLMM",
            "3 3 E",
            "MMRMMRMRRM"
            ];

            // The first line of input will always be plateau size
            string plateauSizeString = input[0];

            // The following lines should be pairs of values, corresponding to a rovers
            // initial position and instructions
            (string initialPosition, string instructions) rover1Input = (input[1], input[2]);
            (string initialPosition, string instructions) rover2Input = (input[3], input[4]);

            RoverPosition rover1FinalPosition = new(1, 3, Direction.North);
            RoverPosition rover2FinalPosition = new(5, 1, Direction.East);

            string expectedRover1Output = "1 3 N";
            string expectedRover2Output = "5 1 E";

            // -- ACT --
            PlateauSize plateauSize;
            InputParser.TryParsePlateauSize(plateauSizeString, out plateauSize);
            Plateau plateau = new Plateau(plateauSize);

            // Rover1
            RoverPosition rover1Position;
            InputParser.TryParseRoverPosition(rover1Input.initialPosition, out rover1Position);
            Rover rover1 = plateau.LandRover(rover1Position);

            RoverInstruction[] rover1Instructions;
            InputParser.TryParseRoverInstructions(rover1Input.instructions, out rover1Instructions);

            rover1.Instruct(rover1Instructions);

            // Rover2
            RoverPosition rover2Position;
            InputParser.TryParseRoverPosition(rover2Input.initialPosition, out rover2Position);
            Rover rover2 = plateau.LandRover(rover2Position);
            RoverInstruction[] rover2Instructions;
            InputParser.TryParseRoverInstructions(rover2Input.instructions, out rover2Instructions);
            rover2.Instruct(rover2Instructions);

            string actualRover1Output;
            string actualRover2Output;

            OutputPaser.TryParseRoverPosition(plateau.GetRoverPosition(rover1), out actualRover1Output);
            OutputPaser.TryParseRoverPosition(plateau.GetRoverPosition(rover2), out actualRover2Output);

            // -- ASSERT --
            plateau.GetRoverAtPos(rover1FinalPosition.X, rover1FinalPosition.Y).Should().Be(rover1);
            plateau.GetRoverAtPos(rover2FinalPosition.X, rover2FinalPosition.Y).Should().Be(rover2);
            rover1.Direction.Should().Be(rover1FinalPosition.Direction);
            rover2.Direction.Should().Be(rover2FinalPosition.Direction);
            actualRover1Output.Should().Be(expectedRover1Output);
            actualRover2Output.Should().Be(expectedRover2Output);
        }
    }
}
