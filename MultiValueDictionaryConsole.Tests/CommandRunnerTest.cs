using Moq;
using MultiValueDictionaryConsole.Commands;
using MultiValueDictionaryConsole.Helpers;
using NUnit.Framework;

namespace MultiValueDictionaryConsole.Tests
{
	internal class CommandRunnerTest
	{
		[Test]
		public void Run_GivenInvalidCommand_WritesError()
		{
			var commandTypeProvider = Mock.Of<ICommandTypeProvider>();

			var expectedErrorMessage = "expectedErrorMessage";
			var command = Mock.Of<ICommand>(m =>
				m.IsValid(out expectedErrorMessage) == false
			);

			var commandFactory = Mock.Of<ICommandFactory>(m =>
				m.Get(It.IsAny<CommandType>(), It.IsAny<string>()) == command
			);

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new CommandRunner(commandTypeProvider, commandFactory, consoleHelper)
				.Run("commandText");

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteError(expectedErrorMessage), Times.Once);

			Mock.Get(command)
				.Verify(m => m.Execute(It.IsAny<MultiValueDictionary>()), Times.Never);
		}

		[Test]
		public void Run_GivenValidCommand_ExecutesCommand()
		{
			var commandTypeProvider = Mock.Of<ICommandTypeProvider>();

			var unsetErrorMessage = "unsetErrorMessage";
			var command = Mock.Of<ICommand>(m =>
				m.IsValid(out unsetErrorMessage) == true
			);

			var commandFactory = Mock.Of<ICommandFactory>(m =>
				m.Get(It.IsAny<CommandType>(), It.IsAny<string>()) == command
			);

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new CommandRunner(commandTypeProvider, commandFactory, consoleHelper)
				.Run("commandText");

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteError(It.IsAny<string>()), Times.Never);

			Mock.Get(command)
				.Verify(m => m.Execute(It.IsAny<MultiValueDictionary>()), Times.Once);
		}
	}
}