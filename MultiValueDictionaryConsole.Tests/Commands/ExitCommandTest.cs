using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using NUnit.Framework;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class ExitCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.ZeroArgumentCommand(CommandType.Exit, commandText, out expectedErrorMessage) == false
			);

			new ExitCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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
				m.ZeroArgumentCommand(CommandType.Exit, commandText, out errorMessage) == true
			);

			new ExitCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out errorMessage)
				.Should().BeTrue();
		}
	}
}
