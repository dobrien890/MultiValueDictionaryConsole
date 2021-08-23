using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using NUnit.Framework;
using System.Collections.Generic;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class AllMembersCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.ZeroArgumentCommand(CommandType.AllMembers, commandText, out expectedErrorMessage) == false
			);

			new AllMembersCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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
				m.ZeroArgumentCommand(CommandType.AllMembers, commandText, out errorMessage) == true
			);

			new AllMembersCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void Execute_GivenNoMembers_WritesNoMembersMessage()
		{
			var dictionary = new MultiValueDictionary
			{
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new AllMembersCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.AllMembers}"
				)
				.Execute(dictionary);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteResponse(AllMembersCommand.NoMembersMessage), Times.Once);
		}

		[Test]
		public void Execute_GivenMembers_WritesAllMembers()
		{
			var keyCount = 2;
			var memberCount = 3;

			var members = new List<string>();
			for (int i = 0; i < memberCount; i++)
			{
				members.Add($"member{i}");
			}

			var dictionary = new MultiValueDictionary();
			for (int i = 0; i < keyCount; i++)
			{
				dictionary.Add($"key{i}", members);
			}

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new AllMembersCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.AllMembers}"
				)
				.Execute(dictionary);

			for (int i = 0; i < memberCount; i++)
			{
				Mock.Get(consoleHelper)
					.Verify(m => m.WriteResponse($"member{i}"), Times.Exactly(keyCount));
			}
		}
	}
}
