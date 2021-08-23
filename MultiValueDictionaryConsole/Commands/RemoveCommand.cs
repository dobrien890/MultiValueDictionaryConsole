namespace MultiValueDictionaryConsole.Commands
{
	internal class RemoveCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string NoKeyErrorMessage = "Key does not exist";
		public static string NoMemberErrorMessage = "Member does not exist";
		public static string MemberRemovedMessage = "Removed";

		public RemoveCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public RemoveCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			var (key, member) = ArgumentHelper.GetKeyAndMember(commandText);

			if (!multiValueDictionary.TryGetValue(key, out var members))
			{
				consoleHelper.WriteError(NoKeyErrorMessage);
				return;
			}

			if (!members.Contains(member))
			{
				consoleHelper.WriteError(NoMemberErrorMessage);
				return;
			}

			if (members.Count == 1)
			{
				multiValueDictionary.Remove(key);
			}
			else
			{
				members.Remove(member);
			}

			consoleHelper.WriteSuccess(MemberRemovedMessage);
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.TwoArgumentCommand(CommandType.Remove, commandText, out errorMessage);
	}
}
