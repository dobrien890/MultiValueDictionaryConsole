using System;

namespace MultiValueDictionaryConsole
{
	class Program
	{
		private static CommandTypeProvider commandTypeProvider = new CommandTypeProvider();
		private static CommandRunner commandExecutor = new CommandRunner();

		static void Main(string[] args)
		{
			WritePreamble();
			while (true)
			{
				var commandText = Console.ReadLine().Trim();
				var commandType = commandTypeProvider.Get(commandText);
				commandExecutor.Run(commandType, commandText);
			}
		}

		private static void WritePreamble()
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("Multi-Value Dictionary Application");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("Enter 'Help' for command options");
			Console.ResetColor();
		}
	}
}
