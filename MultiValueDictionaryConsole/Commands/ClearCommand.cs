using MultiValueDictionaryConsole.Helpers;

namespace MultiValueDictionaryConsole.Commands
{
	internal class ClearCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string ClearedKeysMessage = "Cleared";

		public ClearCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public ClearCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			multiValueDictionary.Clear();
			consoleHelper.WriteSuccess(ClearedKeysMessage);
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.ZeroArgumentCommand(CommandType.Clear, commandText, out errorMessage);
	}
}
