using System;

namespace MultiValueDictionaryConsole
{
	internal class CommandTextValidator : ICommandTextValidator
	{
		public bool ZeroArgumentCommand(CommandType commandType, string commandText, out string errorMessage) =>
			ValidateCommandText(commandType, commandText, 0, out errorMessage);

		public bool OneArgumentCommand(CommandType commandType, string commandText, out string errorMessage) =>
			ValidateCommandText(commandType, commandText, 1, out errorMessage);

		public bool TwoArgumentCommand(CommandType commandType, string commandText, out string errorMessage) =>
			ValidateCommandText(commandType, commandText, 2, out errorMessage);

		private bool ValidateCommandText(CommandType commandType, string commandText, int expectedArgumentCount, out string errorMessage)
		{
			var parameters = commandText.Split(' ');

			if (!ValidateCommandType(commandType, parameters[0], out errorMessage))
				return false;

			if (!ValidateArgumentCount(commandType, parameters.Length, expectedArgumentCount, out errorMessage))
				return false;

			errorMessage = string.Empty;
			return true;
		}

		private bool ValidateCommandType(CommandType commandType, string commandTypeText, out string errorMessage)
		{
			if (!commandTypeText.Equals(commandType.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				errorMessage = $"Command Text [{commandTypeText}] does not match expected Command [{commandType}].";
				return false;
			}

			errorMessage = string.Empty;
			return true;
		}

		private bool ValidateArgumentCount(CommandType commandType, int parameterCount, int expectedArgumentCount, out string errorMessage)
		{
			var argumentCount = parameterCount - 1;
			if (argumentCount > expectedArgumentCount)
			{
				errorMessage = $"Too many arguments specified for Command [{commandType}]. Expected [{expectedArgumentCount}], found [{argumentCount}]";
				return false;
			}

			if (argumentCount < expectedArgumentCount)
			{
				errorMessage = $"Too few arguments specified for Command [{commandType}]. Expected [{expectedArgumentCount}], found [{argumentCount}]";
				return false;
			}

			errorMessage = string.Empty;
			return true;
		}
	}
}
