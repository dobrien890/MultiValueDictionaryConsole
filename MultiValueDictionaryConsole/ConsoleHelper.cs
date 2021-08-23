using System;

namespace MultiValueDictionaryConsole
{
	internal class ConsoleHelper : IConsoleHelper
	{
		public void WriteError(string message) => 
			WriteWithColor($"ERROR: {message}", ConsoleColor.Red);

		public void WriteSuccess(string message) =>
			WriteWithColor(message, ConsoleColor.Green);

		public void WriteResponse(string message) => 
			WriteWithColor(message, ConsoleColor.Cyan);

		private void WriteWithColor(string message, ConsoleColor textColor)
		{
			var currentColor = Console.ForegroundColor;
			Console.ForegroundColor = textColor;
			Console.WriteLine(message);
			Console.ForegroundColor = currentColor;
		}
	}
}
