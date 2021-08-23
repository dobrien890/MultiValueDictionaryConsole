using MultiValueDictionaryConsole.Helpers;

namespace MultiValueDictionaryConsole.Commands
{
	internal class ItemsCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string NoItemsMessage = "No items exist";

		public ItemsCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public ItemsCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			if (multiValueDictionary.Count == 0)
			{
				consoleHelper.WriteResponse(NoItemsMessage);
				return;
			}

			foreach (var entry in multiValueDictionary)
			foreach (var member in entry.Value)
			{
				consoleHelper.WriteResponse($"{entry.Key}: {member}");
			}
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.ZeroArgumentCommand(CommandType.Items, commandText, out errorMessage);
	}
}
