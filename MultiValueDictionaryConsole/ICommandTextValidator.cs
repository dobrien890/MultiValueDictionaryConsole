namespace MultiValueDictionaryConsole
{
	internal interface ICommandTextValidator
	{
		/// <summary>
		/// Validates the command text contains the expected Command Type and no additional arguments.
		/// Returns true if valid, or false with an error message if invalid.
		/// </summary>
		/// <param name="commandType">Expected Command Type</param>
		/// <param name="commandText">Command text to validate</param>
		/// <param name="errorMessage">A message detailing why the command text is invalid.</param>
		/// <returns>True if command text is valid, else false.</returns>
		bool ZeroArgumentCommand(CommandType commandType, string commandText, out string errorMessage);

		/// <summary>
		/// Validates the command text contains the expected Command Type and one argument.
		/// Returns true if valid, or false with an error message if invalid.
		/// </summary>
		/// <param name="commandType">Expected Command Type</param>
		/// <param name="commandText">Command text to validate</param>
		/// <param name="errorMessage">A message detailing why the command text is invalid.</param>
		/// <returns>True if command text is valid, else false.</returns>
		bool OneArgumentCommand(CommandType commandType, string commandText, out string errorMessage);

		/// <summary>
		/// Validates the command text contains the expected Command Type and two arguments.
		/// Returns true if valid, or false with an error message if invalid.
		/// </summary>
		/// <param name="commandType">Expected Command Type</param>
		/// <param name="commandText">Command text to validate</param>
		/// <param name="errorMessage">A message detailing why the command text is invalid.</param>
		/// <returns>True if command text is valid, else false.</returns>
		bool TwoArgumentCommand(CommandType commandType, string commandText, out string errorMessage);
	}
}