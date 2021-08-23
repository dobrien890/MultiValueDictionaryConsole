using FluentAssertions;
using MultiValueDictionaryConsole.Commands;
using NUnit.Framework;
using System;

namespace MultiValueDictionaryConsole.Tests.Commands
{
	internal class UnknownCommandTest
	{
		[Test]
		public void IsValid_ReturnsFalseWithMessageContainingUnknownCommand()
		{
			var unknownCommand = "unknownCommand";
			new UnknownCommand($"{unknownCommand} with arguments")
				.IsValid(out var errorMessage)
				.Should().BeFalse();

			errorMessage
				.Should().Contain(unknownCommand);
		}

		[Test]
		public void Execute_ShouldThrowNotSupportedException()
		{
			new Action(() =>
					new UnknownCommand("commandText")
						.Execute(new MultiValueDictionary())
				)
				.Should().ThrowExactly<NotSupportedException>();
		}
	}
}
