using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using NUnit.Framework;
using System.Collections.Generic;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class KeysCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.ZeroArgumentCommand(CommandType.Keys, commandText, out expectedErrorMessage) == false
			);

			new KeysCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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
				m.ZeroArgumentCommand(CommandType.Keys, commandText, out errorMessage) == true
			);

			new KeysCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void Execute_GivenNoKeys_WritesNoKeysMessage()
		{
			var dictionary = new MultiValueDictionary();

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new KeysCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Keys}"
				)
				.Execute(dictionary);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteResponse(KeysCommand.NoKeysMessage), Times.Once);
		}

		[TestCase(1)]
		[TestCase(5)]
		public void Execute_GivenKeys_WritesEachKey(int keyCount)
		{
			var dictionary = new MultiValueDictionary();

			for (int i = 0; i < keyCount; i++)
			{
				dictionary.Add($"key{i}", new List<string>());
			}

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new KeysCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Keys}"
				)
				.Execute(dictionary);

			for (int i = 0; i < keyCount; i++)
			{
				Mock.Get(consoleHelper)
					.Verify(m => m.WriteResponse($"key{i}"), Times.Once);
			}
		}
	}
}
