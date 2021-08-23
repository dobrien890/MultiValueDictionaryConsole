namespace MultiValueDictionaryConsole
{
	internal interface ICommandTextValidator
	{
		bool OneArgumentCommand(CommandType commandType, string commandText, out string errorMessage);
		bool TwoArgumentCommand(CommandType commandType, string commandText, out string errorMessage);
		bool ZeroArgumentCommand(CommandType commandType, string commandText, out string errorMessage);
	}
}