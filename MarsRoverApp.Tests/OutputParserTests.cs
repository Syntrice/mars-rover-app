﻿using FluentAssertions;
using MarsRoverApp.Input;

namespace MarsRoverApp.Tests
{

    [TestFixture]
    public class OutputParserTests
    {
        [TestCase(0, 0, Direction.North, "0 0 N")]
        [TestCase(1, 2, Direction.East, "1 2 E")]
        [TestCase(2, 1, Direction.South, "2 1 S")]
        [TestCase(5, 5, Direction.West, "5 5 W")]
        public void ParseRoverPosition_WithValidInput_ReturnsExpectedString(int x, int y, Direction direction, string expected)
        {
            // Arrange
            RoverPosition input = new RoverPosition(x, y, direction);

            // Act
            string actual = OutputPaser.ParseRoverPosition(input);

            // Assert
            actual.Should().Be(expected);
        }
    }
}
