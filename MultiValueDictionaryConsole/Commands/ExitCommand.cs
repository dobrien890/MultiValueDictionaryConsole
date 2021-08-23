using MultiValueDictionaryConsole.Helpers;
using System;

namespace MultiValueDictionaryConsole.Commands
{
	internal class ExitCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string ExitingMessage = "Exiting application";

		public ExitCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public ExitCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			consoleHelper.WriteResponse(ExitingMessage);
			Environment.Exit(0);
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.ZeroArgumentCommand(CommandType.Exit, commandText, out errorMessage);
	}
}
