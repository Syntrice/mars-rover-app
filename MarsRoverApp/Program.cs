using MarsRoverApp;
using MarsRoverApp.ConsoleGUI;

Application application = new Application();
State state = new PlateauCreationState(application);
application.Run(state);