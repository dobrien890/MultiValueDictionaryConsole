using MultiValueDictionaryConsole.Helpers;

namespace MultiValueDictionaryConsole.Commands
{
	internal class MemberExistsCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string MemberExistsMessage = "true";
		public static string MemberDoesNotExistMessage = "false";

		public MemberExistsCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public MemberExistsCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
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
				consoleHelper.WriteResponse(MemberDoesNotExistMessage);
				return;
			}

			if (!members.Contains(member))
			{
				consoleHelper.WriteResponse(MemberDoesNotExistMessage);
				return;
			}

			consoleHelper.WriteResponse(MemberExistsMessage);
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.TwoArgumentCommand(CommandType.MemberExists, commandText, out errorMessage);
	}
}
