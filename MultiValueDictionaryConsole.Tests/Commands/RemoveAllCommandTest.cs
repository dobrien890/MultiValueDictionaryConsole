using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using MultiValueDictionaryConsole.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class RemoveAllCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.OneArgumentCommand(CommandType.RemoveAll, commandText, out expectedErrorMessage) == false
			);

			new RemoveAllCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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
				m.OneArgumentCommand(CommandType.RemoveAll, commandText, out errorMessage) == true
			);

			new RemoveAllCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void Execute_GivenNoKey_WritesNoKeyErrorMessage()
		{
			var key = "key";
			var dictionary = new MultiValueDictionary
			{
				{ "otherKey", new List<string> { "member" } }
			};

			var expectedResult = new MultiValueDictionary
			{
				{ "otherKey", new List<string> { "member" } }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new RemoveAllCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.RemoveAll} {key}"
				)
				.Execute(dictionary);

			dictionary
				.Should().BeEquivalentTo(expectedResult);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteError(RemoveCommand.NoKeyErrorMessage), Times.Once);
		}

		[Test]
		public void Execute_GivenExistingKey_RemovesKey()
		{
			var key = "key";
			var dictionary = new MultiValueDictionary
			{
				{ key, new List<string> { "member", "otherMember" } },
				{ "otherKey", new List<string> { "member" } }
			};

			var expectedResult = new MultiValueDictionary
			{
				{ "otherKey", new List<string> { "member" } }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new RemoveAllCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.RemoveAll} {key}"
				)
				.Execute(dictionary);

			dictionary
				.Should().BeEquivalentTo(expectedResult);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteSuccess(RemoveCommand.MemberRemovedMessage), Times.Once);
		}
	}
}
