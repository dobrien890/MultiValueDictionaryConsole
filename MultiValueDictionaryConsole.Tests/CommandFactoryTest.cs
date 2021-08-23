using FluentAssertions;
using NUnit.Framework;
using System;

namespace MultiValueDictionaryConsole.Tests
{
	internal class CommandFactoryTest
	{
		[Test]
		public void Get_GivenInvalidCommand_ThrowsArgumentException()
		{
			new Action(() =>
					new CommandFactory()
						.Get((CommandType)13, "commandText")
				)
				.Should().ThrowExactly<ArgumentException>();
		}
	}
}