namespace MultiValueDictionaryConsole.Commands
{
	internal interface ICommand
	{
		/// <summary>
		/// Executes the command against the provided dictionary.
		/// </summary>
		/// <param name="multiValueDictionary">Dictionary to execute the command against.</param>
		void Execute(MultiValueDictionary multiValueDictionary);

		/// <summary>
		/// Returns true if the command is valid, or false with an error message detailing why.
		/// </summary>
		/// <param name="errorMessage">Message explaining why the command is invalid if false is returned.</param>
		/// <returns>Bool indicating if the command is valid and can be executed.</returns>
		bool IsValid(out string errorMessage);
	}
}
