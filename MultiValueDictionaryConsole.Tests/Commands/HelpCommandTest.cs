using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using NUnit.Framework;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class HelpCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.ZeroArgumentCommand(CommandType.Help, commandText, out expectedErrorMessage) == false
			);

			new HelpCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out var errorMessage)
				.Should().BeFalse();

			errorMessage
				.Should().Be(expectedErrorMessage);
		}

		[Test]
		public void IsValid_GivenValid_ReturnsTrue()
		{
			var commandText = "commandText";
			var errorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.ZeroArgumentCommand(CommandType.Help, commandText, out errorMessage) == true
			);

			new HelpCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void Execute_WritesHelpText()
		{
			var consoleHelper = Mock.Of<IConsoleHelper>();

			new HelpCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Help}"
				)
				.Execute(new MultiValueDictionary());

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteResponse(HelpCommand.HelpText), Times.Once);
		}
	}
}
