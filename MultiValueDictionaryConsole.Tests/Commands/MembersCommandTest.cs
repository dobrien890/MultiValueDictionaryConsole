using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using MultiValueDictionaryConsole.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class MembersCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.OneArgumentCommand(CommandType.Members, commandText, out expectedErrorMessage) == false
			);

			new MembersCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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
				m.OneArgumentCommand(CommandType.Members, commandText, out errorMessage) == true
			);

			new MembersCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void Execute_GivenNoKey_WritesNoKeyErrorMessage()
		{
			var dictionary = new MultiValueDictionary();

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new MembersCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Members} key"
				)
				.Execute(dictionary);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteError(MembersCommand.NoKeyErrorMessage), Times.Once);
		}

		[TestCase(1)]
		[TestCase(5)]
		public void Execute_GivenMembers_WritesEachMember(int memberCount)
		{
			var members = new List<string>();
			for (int i = 0; i < memberCount; i++)
			{
				members.Add($"member{i}");
			}

			var key = "key";

			var dictionary = new MultiValueDictionary
			{
				{ key, members }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new MembersCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Members} {key}"
				)
				.Execute(dictionary);

			for (int i = 0; i < memberCount; i++)
			{
				Mock.Get(consoleHelper)
					.Verify(m => m.WriteResponse($"member{i}"), Times.Once);
			}
		}
	}
}
