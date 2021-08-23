using MultiValueDictionaryConsole.Commands;

namespace MultiValueDictionaryConsole
{
	internal interface ICommandFactory
	{
		ICommand Get(CommandType commandType, string commandText);
	}
}
