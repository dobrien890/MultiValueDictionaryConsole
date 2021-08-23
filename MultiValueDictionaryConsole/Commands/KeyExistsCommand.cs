namespace MultiValueDictionaryConsole.Commands
{
	internal class KeyExistsCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string KeyExistsMessage = "true";
		public static string KeyDoesNotExistMessage = "false";

		public KeyExistsCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public KeyExistsCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			var key = ArgumentHelper.GetKey(commandText);

			if (multiValueDictionary.ContainsKey(key))
			{
				consoleHelper.WriteResponse(KeyExistsMessage);
			}
			else
			{
				consoleHelper.WriteResponse(KeyDoesNotExistMessage);
			}
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.OneArgumentCommand(CommandType.KeyExists, commandText, out errorMessage);
	}
}
