namespace MultiValueDictionaryConsole.Commands
{
	internal class RemoveAllCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string NoKeyErrorMessage = "Key does not exist";
		public static string KeyRemovedMessage = "Removed";

		public RemoveAllCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public RemoveAllCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			var key = ArgumentHelper.GetKey(commandText);

			if (!multiValueDictionary.ContainsKey(key))
			{
				consoleHelper.WriteError(NoKeyErrorMessage);
				return;
			}

			multiValueDictionary.Remove(key);
			consoleHelper.WriteSuccess(KeyRemovedMessage);
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.OneArgumentCommand(CommandType.RemoveAll, commandText, out errorMessage);
	}
}
