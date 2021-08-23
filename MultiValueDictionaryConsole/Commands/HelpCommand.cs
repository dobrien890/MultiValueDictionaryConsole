namespace MultiValueDictionaryConsole.Commands
{
	internal class HelpCommand : ICommand
	{
		private readonly ICommandTextValidator commandTextValidator;
		private readonly IConsoleHelper consoleHelper;
		private readonly string commandText;

		private static readonly string keyArg = "Key";
		private static readonly string memberArg = "Member";
		public static string HelpText => $@"
Arguments:
	{keyArg}
	- Identifier for a collection

	{memberArg}
	- Item in a collection

Commands:
	{CommandType.Add} {{{keyArg}}} {{{memberArg}}}
	- Adds a member to a collection for a given key. Displays an error if the member already exists for the key.

	{CommandType.AllMembers}
	- Returns all the members in the dictionary. Returns nothing if there are none. Order is not guaranteed.

	{CommandType.Clear}
	- Remove all keys and members from dictionary

	{CommandType.Items}
	- Returns all keys in the dictionary and all of their members. Returns nothing if there are none. Order is not guaranteed.
	
	{CommandType.KeyExists} {{{keyArg}}}
	- Returns whether a key exists or not.

	{CommandType.Keys}
	- Returns all the keys in the dictionary. Order is not guaranteed.

	{CommandType.MemberExists} {{{keyArg}}} {{{memberArg}}}
	- Returns whether a member exists within a key. Returns false if the key does not exist.

	{CommandType.Members} {{{keyArg}}}
	- Returns the collection of strings for the given key. Return order is not guaranteed. Returns an error if the key does not exists.

	{CommandType.Remove} {{{keyArg}}} {{{memberArg}}}
	- Removes a member from a key. If the last member is removed from the key, the key is removed from the dictionary. If the key or member does not exist, displays an error.

	{CommandType.RemoveAll} {{{keyArg}}}
	- Removes all members for a key and removes the key from the dictionary. Returns an error if the key does not exist.
	
	{CommandType.Help}
	- List commands and arguments for application

	{CommandType.Exit}
	- Exit application
";

		public HelpCommand(string commandText)
			: this(new CommandTextValidator(), new ConsoleHelper(), commandText)
		{
		}

		public HelpCommand(ICommandTextValidator commandTextValidator, IConsoleHelper consoleHelper, string commandText)
		{
			this.commandTextValidator = commandTextValidator;
			this.consoleHelper = consoleHelper;
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			consoleHelper.WriteResponse(HelpText);
		}

		public bool IsValid(out string errorMessage) =>
			commandTextValidator.ZeroArgumentCommand(CommandType.Help, commandText, out errorMessage);
	}
}
