using FluentAssertions;
using NUnit.Framework;

namespace MultiValueDictionaryConsole.Tests
{
	internal class CommandTextValidatorTest
	{
		private CommandTextValidator commandTextValidator;

		[SetUp]
		public void SetUp()
		{
			commandTextValidator = new CommandTextValidator();
		}

		[Test]
		public void ZeroArgumentCommand_GivenCommandTypeMismatch_ReturnsFalse()
		{
			var commandText = "InvalidCommand";

			commandTextValidator
				.ZeroArgumentCommand(CommandType.Add, commandText, out var errorMessage)
				.Should().BeFalse();
		}

		[Test]
		public void ZeroArgumentCommand_GivenTooManyArguments_ReturnsFalse()
		{
			var commandType = CommandType.Add;
			var commandText = $"{commandType} arg1";

			commandTextValidator
				.ZeroArgumentCommand(commandType, commandText, out var errorMessage)
				.Should().BeFalse();
		}

		[Test]
		public void ZeroArgumentCommand_GivenZeroArguments_ReturnsTrue()
		{
			var commandType = CommandType.Add;
			var commandText = $"{commandType}";

			commandTextValidator
				.ZeroArgumentCommand(commandType, commandText, out var errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void OneArgumentCommand_GivenCommandTypeMismatch_ReturnsFalse()
		{
			var commandText = "InvalidCommand";

			commandTextValidator
				.OneArgumentCommand(CommandType.Add, commandText, out var errorMessage)
				.Should().BeFalse();
		}

		[Test]
		public void OneArgumentCommand_GivenTooFewArguments_ReturnsFalse()
		{
			var commandType = CommandType.Add;
			var commandText = $"{commandType}";

			commandTextValidator
				.OneArgumentCommand(commandType, commandText, out var errorMessage)
				.Should().BeFalse();
		}

		[Test]
		public void OneArgumentCommand_GivenTooManyArguments_ReturnsFalse()
		{
			var commandType = CommandType.Add;
			var commandText = $"{commandType} arg1 arg2";

			commandTextValidator
				.OneArgumentCommand(commandType, commandText, out var errorMessage)
				.Should().BeFalse();
		}

		[Test]
		public void OneArgumentCommand_GivenOneArgument_ReturnsTrue()
		{
			var commandType = CommandType.Add;
			var commandText = $"{commandType} arg1";

			commandTextValidator
				.OneArgumentCommand(commandType, commandText, out var errorMessage)
				.Should().BeTrue();
		}

		[Test]
		public void TwoArgumentCommand_GivenCommandTypeMismatch_ReturnsFalse()
		{
			var commandText = "InvalidCommand";

			commandTextValidator
				.TwoArgumentCommand(CommandType.Add, commandText, out var errorMessage)
				.Should().BeFalse();
		}

		[Test]
		public void TwoArgumentCommand_GivenTooFewArguments_ReturnsFalse()
		{
			var commandType = CommandType.Add;
			var commandText = $"{commandType} arg1";

			commandTextValidator
				.TwoArgumentCommand(commandType, commandText, out var errorMessage)
				.Should().BeFalse();
		}

		[Test]
		public void TwoArgumentCommand_GivenTooManyArguments_ReturnsFalse()
		{
			var commandType = CommandType.Add;
			var commandText = $"{commandType} arg1 arg2 arg3";

			commandTextValidator
				.TwoArgumentCommand(commandType, commandText, out var errorMessage)
				.Should().BeFalse();
		}

		[Test]
		public void TwoArgumentCommand_GivenTwoArguments_ReturnsTrue()
		{
			var commandType = CommandType.Add;
			var commandText = $"{commandType} arg1 arg2";

			commandTextValidator
				.TwoArgumentCommand(commandType, commandText, out var errorMessage)
				.Should().BeTrue();
		}
	}
}