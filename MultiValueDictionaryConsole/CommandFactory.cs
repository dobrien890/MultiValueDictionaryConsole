using MultiValueDictionaryConsole.Commands;
using System;

namespace MultiValueDictionaryConsole
{
	internal class CommandFactory : ICommandFactory
	{
		public ICommand Get(CommandType commandType, string commandText) =>
			commandType switch
			{
				CommandType.Keys => new KeysCommand(commandText),
				CommandType.Members => new MembersCommand(commandText),
				CommandType.Add => new AddCommand(commandText),
				CommandType.Remove => new RemoveCommand(commandText),
				CommandType.RemoveAll => new RemoveAllCommand(commandText),
				CommandType.Clear => new ClearCommand(commandText),
				CommandType.KeyExists => new KeyExistsCommand(commandText),
				CommandType.MemberExists => new MemberExistsCommand(commandText),
				CommandType.AllMembers => new AllMembersCommand(commandText),
				CommandType.Items => new ItemsCommand(commandText),
				CommandType.Help => new HelpCommand(commandText),
				CommandType.Exit => new ExitCommand(commandText),
				CommandType.Unknown => new UnknownCommand(commandText),
				_ => throw new ArgumentException($"[{commandType}] is not a supported Command Type")
			};
	}
}
