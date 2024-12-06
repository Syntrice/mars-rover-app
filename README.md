# Mars Rover Application

## Description

This Application is designed to solve a kata-style problem using
test-driven development, while developing a full application. The aim
of the kata is to implement a system which can move "rovers" across
the "surface of mars" according to sequences of commands. It is a task
set by [Northcoders](https://northcoders.com/) as part of their
C# / .NET software engineering bootcamp.

## Development

This project features various software development practices, such as

- Test Driven Development (TDD)
  - Unit and Integration Testing using [NUnit](https://nunit.org/)
  - Mocking using [Moq](https://github.com/devlooped/moq)
- Design Patterns
  - [Observer Design Pattern](https://refactoring.guru/design-patterns/observer)
- Layered Architecture
  - Logic Layer
  - Input Layer

## Build Instructions

The easiest way to build this project is to install Microsoft Visual
Studio 2022, along with DOTNET 8.0. Clone the repository, and then
you should then be able to build the solution by running
`dotnet build .\MarsRoverApp.sln` from the repository root folder.

## Commit messages

Commit messages in this repository follow the [Conventional Commits Specification](https://www.conventionalcommits.org/en/v1.0.0/)
