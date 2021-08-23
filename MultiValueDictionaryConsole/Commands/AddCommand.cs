using System.Collections.Generic;

namespace MultiValueDictionaryConsole.Commands
{
	internal class AddCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		public static string AddedMemberMessage = "Added";
		public static string MemberExistsErrorMessage = "Member already exists for this key";

		public AddCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public AddCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			var (key, member) = ArgumentHelper.GetKeyAndMember(commandText);

			if (multiValueDictionary.TryGetValue(key, out var members))
			{
				if (members.Contains(member))
				{
					consoleHelper.WriteError(MemberExistsErrorMessage);
					return;
				}
				else
				{
					members.Add(member);
				}
			}
			else
			{
				multiValueDictionary.Add(key, new List<string> { member });
			}

			consoleHelper.WriteSuccess(AddedMemberMessage);
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.TwoArgumentCommand(CommandType.Add, commandText, out errorMessage);
	}
}
