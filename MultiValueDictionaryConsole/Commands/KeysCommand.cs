namespace MultiValueDictionaryConsole.Commands
{
	internal class KeysCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string NoKeysMessage = "No keys in dictionary";

		public KeysCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public KeysCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			if (multiValueDictionary.Keys.Count == 0)
			{
				consoleHelper.WriteResponse(NoKeysMessage);
				return;
			}

			foreach (var key in multiValueDictionary.Keys)
			{
				consoleHelper.WriteResponse(key);
			}
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.ZeroArgumentCommand(CommandType.Keys, commandText, out errorMessage);
	}
}
