namespace MultiValueDictionaryConsole.Helpers
{
	internal interface IConsoleHelper
	{
		/// <summary>
		/// Write a message to console in red, prepended with 'ERROR:'.
		/// </summary>
		/// <param name="message">Message to write to console</param>
		void WriteError(string message);

		/// <summary>
		/// Write a message to console in green.
		/// </summary>
		/// <param name="message">Message to write to console</param>
		void WriteSuccess(string message);

		/// <summary>
		/// Write a message to console in cyan.
		/// </summary>
		/// <param name="message">Message to write to console</param>
		void WriteResponse(string message);
	}
}