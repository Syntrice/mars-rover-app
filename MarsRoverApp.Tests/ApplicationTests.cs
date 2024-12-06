using Moq;

namespace MarsRoverApp.Tests
{
    [TestFixture]
    internal class ApplicationTests
    {
        [Test]
        public void Application_RunsState()
        {
            // Arrange
            Application app = new Application();
            Mock<IState> state = new Mock<IState>();
            state.Setup(m => m.Run()).Callback(() => app.Stop());

            // Act
            app.Run(state.Object);

            // Assert
            state.Verify(state => state.Run(), Times.Once);
        }

    }
}
