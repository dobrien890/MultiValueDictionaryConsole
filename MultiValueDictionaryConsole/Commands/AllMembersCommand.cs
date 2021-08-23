using System.Linq;

namespace MultiValueDictionaryConsole.Commands
{
	internal class AllMembersCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string NoMembersMessage = "No members exist";

		public AllMembersCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public AllMembersCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			if (multiValueDictionary.Count == 0)
			{
				consoleHelper.WriteResponse(NoMembersMessage);
				return;
			}

			foreach (var member in multiValueDictionary.SelectMany(d => d.Value))
			{
				consoleHelper.WriteResponse(member);
			}
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.ZeroArgumentCommand(CommandType.AllMembers, commandText, out errorMessage);
	}
}
