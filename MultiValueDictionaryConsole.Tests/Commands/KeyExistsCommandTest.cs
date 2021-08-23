using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using MultiValueDictionaryConsole.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class KeyExistsCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.OneArgumentCommand(CommandType.KeyExists, commandText, out expectedErrorMessage) == false
			);

			new KeyExistsCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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
				m.OneArgumentCommand(CommandType.KeyExists, commandText, out errorMessage) == true
			);

			new KeyExistsCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void Execute_GivenKeyExists_WritesTrueToConsole()
		{
			var key = "key";
			var dictionary = new MultiValueDictionary
			{
				{ key, new List<string> { "member" } }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new KeyExistsCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Clear} {key}"
				)
				.Execute(dictionary);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteResponse(KeyExistsCommand.KeyExistsMessage), Times.Once);
		}

		[Test]
		public void Execute_GivenKeyDoesNotExist_WritesFalseToConsole()
		{
			var key = "key";
			var dictionary = new MultiValueDictionary
			{
				{ "otherKey", new List<string> { "member" } }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new KeyExistsCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Clear} {key}"
				)
				.Execute(dictionary);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteResponse(KeyExistsCommand.KeyDoesNotExistMessage), Times.Once);
		}
	}
}
