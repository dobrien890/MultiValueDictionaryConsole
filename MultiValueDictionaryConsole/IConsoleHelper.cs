namespace MultiValueDictionaryConsole
{
	internal interface IConsoleHelper
	{
		void WriteError(string message);
		void WriteSuccess(string message);
		void WriteResponse(string message);
	}
}