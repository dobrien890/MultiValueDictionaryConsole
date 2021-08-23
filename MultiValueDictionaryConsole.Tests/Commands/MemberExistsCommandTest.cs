using FluentAssertions;
using Moq;
using MultiValueDictionaryConsole.Commands;
using MultiValueDictionaryConsole.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class MemberExistsCommandTest
	{
		[Test]
		public void IsValid_GivenNotValid_ReturnsFalseWithErrorMessage()
		{
			var commandText = "commandText";
			var expectedErrorMessage = "expectedErrorMessage";
			var commandTextValidator = Mock.Of<ICommandTextValidator>(m =>
				m.TwoArgumentCommand(CommandType.MemberExists, commandText, out expectedErrorMessage) == false
			);

			new MemberExistsCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
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
				m.TwoArgumentCommand(CommandType.MemberExists, commandText, out errorMessage) == true
			);

			new MemberExistsCommand(commandTextValidator, Mock.Of<IConsoleHelper>(), commandText)
				.IsValid(out errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void Execute_GivenMemberExists_WritesTrueToConsole()
		{
			var key = "key";
			var member = "member";
			var dictionary = new MultiValueDictionary
			{
				{ key, new List<string> { member } }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new MemberExistsCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.MemberExists} {key} {member}"
				)
				.Execute(dictionary);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteResponse(MemberExistsCommand.MemberExistsMessage), Times.Once);
		}

		[Test]
		public void Execute_GivenKeyDoesNotExist_WritesFalseToConsole()
		{
			var key = "key";
			var member = "member";
			var dictionary = new MultiValueDictionary
			{
				{ "otherKey", new List<string> { "member" } }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new MemberExistsCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.MemberExists} {key} {member}"
				)
				.Execute(dictionary);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteResponse(MemberExistsCommand.MemberDoesNotExistMessage), Times.Once);
		}

		[Test]
		public void Execute_GivenMemberDoesNotExist_WritesFalseToConsole()
		{
			var key = "key";
			var member = "member";
			var dictionary = new MultiValueDictionary
			{
				{ key, new List<string> { "otherMember" } },
				{ "otherKey", new List<string> { "member" } }
			};

			var consoleHelper = Mock.Of<IConsoleHelper>();

			new MemberExistsCommand(
					Mock.Of<ICommandTextValidator>(),
					consoleHelper,
					$"{CommandType.MemberExists} {key} {member}"
				)
				.Execute(dictionary);

			Mock.Get(consoleHelper)
				.Verify(m => m.WriteResponse(MemberExistsCommand.MemberDoesNotExistMessage), Times.Once);
		}
	}
}
