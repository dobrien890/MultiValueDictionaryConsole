namespace MultiValueDictionaryConsole
{
	internal interface ICommandTypeProvider
	{
		/// <summary>
		/// Gets the Command Type from the inputted text, or unknown if a Command Type cannot be determined.
		/// </summary>
		/// <param name="commandText">Input text to extract command type from.</param>
		/// <returns>Returns the Command Type from the command text, or Unknown Command Type.</returns>
		CommandType Get(string commandText);
	}
}