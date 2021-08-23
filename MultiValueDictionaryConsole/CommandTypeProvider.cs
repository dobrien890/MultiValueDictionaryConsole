using System;

namespace MultiValueDictionaryConsole
{
	internal class CommandTypeProvider
	{
		public CommandType Get(string commandText)
		{
			var command = commandText.Split(' ')[0];
			if (Enum.TryParse(typeof(CommandType), command, true, out var commandType))
				return (CommandType)commandType;
			return CommandType.Unknown;
		}
	}
}
