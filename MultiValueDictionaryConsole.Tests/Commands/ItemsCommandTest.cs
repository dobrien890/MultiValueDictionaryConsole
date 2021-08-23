using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using MultiValueDictionaryConsole.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class ItemsCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.ZeroArgumentCommand(CommandType.Items, commandText, out expectedErrorMessage) == false
			);

			new ItemsCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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
				m.ZeroArgumentCommand(CommandType.Items, commandText, out errorMessage) == true
			);

			new ItemsCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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

			new ItemsCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Items}"
				)
				.Execute(dictionary);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteResponse(ItemsCommand.NoItemsMessage), Times.Once);
		}

		[Test]
		public void Execute_GivenItems_WritesAllItems()
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

			new ItemsCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Items}"
				)
				.Execute(dictionary);

			for (int i = 0; i < keyCount; i++)
			for (int j = 0; j < memberCount; j++)
			{
				Mock.Get(consoleHelper)
					.Verify(m => m.WriteResponse($"key{i}: member{j}"), Times.Once);
			}
		}
	}
}
