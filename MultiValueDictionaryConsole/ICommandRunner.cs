namespace MultiValueDictionaryConsole
{
	internal interface ICommandRunner
	{

		/// <summary>
		/// Attempts to run a command from the command text.
		/// If the command text is invalid, it will write an error message to the console explaining why.
		/// </summary>
		/// <param name="commandText">Command Text to run.</param>
		void Run(string commandText);
	}
}