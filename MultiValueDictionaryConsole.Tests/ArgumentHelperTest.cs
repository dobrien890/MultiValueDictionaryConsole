using FluentAssertions;
using NUnit.Framework;

namespace MultiValueDictionaryConsole.Tests
{
	internal class ArgumentHelperTest
	{
		[Test]
		public void GetKey_GivenCommandTextWithKey_ReturnsKey()
		{
			var key = "key";
			var commandText = $"command {key}";

			ArgumentHelper
				.GetKey(commandText)
				.Should().Be(key);
		}

		[Test]
		public void GetKeyAndMember_GivenCommandTextWithKeyAndMember_ReturnsKeyAndMember()
		{
			var key = "key";
			var member = "member";
			var commandText = $"command {key} {member}";

			ArgumentHelper
				.GetKeyAndMember(commandText)
				.Should().Be((key, member));
		}
	}
}