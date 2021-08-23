using FluentAssertions;
using NUnit.Framework;

namespace MultiValueDictionaryConsole.Tests
{
	internal class CommandTypeProviderTest
	{
		[TestCase("Add")]
		[TestCase("add")]
		[TestCase("ADD")]
		[TestCase("adD")]
		public void Get_GivenValidCommandWithVariousCapitalization_ReturnsCommandType(string commandText)
		{
			new CommandTypeProvider()
				.Get(commandText)
				.Should().Be(CommandType.Add);
		}

		[Test]
		public void Get_GivenValidCommandWithArguments_ReturnsCommandType()
		{
			new CommandTypeProvider()
				.Get("Add with arguments")
				.Should().Be(CommandType.Add);
		}

		[Test]
		public void Get_GivenInvalidCommandWithArguments_ReturnsUnknownCommandType()
		{
			new CommandTypeProvider()
				.Get("InvalidCommand")
				.Should().Be(CommandType.Unknown);
		}
	}
}