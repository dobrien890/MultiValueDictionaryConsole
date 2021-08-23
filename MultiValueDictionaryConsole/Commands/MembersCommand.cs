using MultiValueDictionaryConsole.Helpers;

namespace MultiValueDictionaryConsole.Commands
{
	internal class MembersCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string NoKeyErrorMessage = "Key does not exist";

		public MembersCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public MembersCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			var key = ArgumentHelper.GetKey(commandText);

			if (!multiValueDictionary.TryGetValue(key, out var members))
			{
				consoleHelper.WriteError(NoKeyErrorMessage);
				return;
			}

			foreach (var member in members)
			{
				consoleHelper.WriteResponse(member);
			}
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.OneArgumentCommand(CommandType.Members, commandText, out errorMessage);
	}
}
