using MultiValueDictionaryConsole.Commands;

namespace MultiValueDictionaryConsole
{
	internal interface ICommandFactory
	{
		/// <summary>
		/// Gets an instance of an ICommand based on the Command Type.
		/// </summary>
		/// <param name="commandType">Type of command to return.</param>
		/// <param name="commandText">Text of command to execute.</param>
		/// <returns>Returns an instance of an ICommand that will execute provided command text.</returns>
		ICommand Get(CommandType commandType, string commandText);
	}
}
