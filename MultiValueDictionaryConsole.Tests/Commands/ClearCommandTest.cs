using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using NUnit.Framework;
using System.Collections.Generic;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class ClearCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.ZeroArgumentCommand(CommandType.Clear, commandText, out expectedErrorMessage) == false
			);

			new ClearCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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
				m.ZeroArgumentCommand(CommandType.Clear, commandText, out errorMessage) == true
			);

			new ClearCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void Execute_GivenNoKeys_HasNoKeys()
		{
			var dictionary = new MultiValueDictionary
			{
			};

			var expectedResult = new MultiValueDictionary
			{
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new ClearCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Clear}"
				)
				.Execute(dictionary);

			dictionary
				.Should().BeEquivalentTo(expectedResult);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteSuccess(ClearCommand.ClearedKeysMessage), Times.Once);
		}

		[Test]
		public void Execute_GivenKeys_ClearsKeys()
		{
			var dictionary = new MultiValueDictionary
			{
				{ "key", new List<string> { "member" } },
				{ "otherKey", new List<string> { "member" } },
				{ "otherOtherKey", new List<string> { "member" } }
			};

			var expectedResult = new MultiValueDictionary
			{
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new ClearCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Clear}"
				)
				.Execute(dictionary);

			dictionary
				.Should().BeEquivalentTo(expectedResult);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteSuccess(ClearCommand.ClearedKeysMessage), Times.Once);
		}
	}
}
