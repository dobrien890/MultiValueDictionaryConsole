using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using MultiValueDictionaryConsole.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class AddCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.TwoArgumentCommand(CommandType.Add, commandText, out expectedErrorMessage) == false
			);

			new AddCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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
				m.TwoArgumentCommand(CommandType.Add, commandText, out errorMessage) == true
			);

			new AddCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void Execute_GivenNewKeyMember_AddsKeyMember()
		{
			var key = "key";
			var member = "member";

			var dictionary = new MultiValueDictionary();

			var expectedResult = new MultiValueDictionary
			{
				{ key, new List<string> { member } }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new AddCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Add} {key} {member}"
				)
				.Execute(dictionary);

			dictionary
				.Should().BeEquivalentTo(expectedResult);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteSuccess(AddCommand.AddedMemberMessage), Times.Once);
		}

		[Test]
		public void Execute_GivenExistingKeyWithNewMember_AddsKeyMember()
		{
			var key = "key";
			var newMember = "newMember";
			var existingMember = "existingMember";

			var dictionary = new MultiValueDictionary
			{
				{ key, new List<string> { existingMember } }
			};

			var expectedResult = new MultiValueDictionary
			{
				{ key, new List<string> { existingMember , newMember } }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new AddCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Add} {key} {newMember}"
				)
				.Execute(dictionary);

			dictionary
				.Should().BeEquivalentTo(expectedResult);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteSuccess(AddCommand.AddedMemberMessage), Times.Once);
		}

		[Test]
		public void Execute_GivenExistingKeyMember_WritesErrorToLog()
		{
			var key = "key";
			var existingMember = "existingMember";

			var dictionary = new MultiValueDictionary
			{
				{ key, new List<string> { existingMember } }
			};

			var expectedResult = new MultiValueDictionary
			{
				{ key, new List<string> { existingMember } }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new AddCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.Add} {key} {existingMember}"
				)
				.Execute(dictionary);

			dictionary
				.Should().BeEquivalentTo(expectedResult);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteError(AddCommand.MemberExistsErrorMessage), Times.Once);
		}
	}
}
