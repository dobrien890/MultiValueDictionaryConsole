namespace MultiValueDictionaryConsole
{
	internal class CommandRunner
	{
		private readonly ICommandFactory commandFactory;
		private readonly IConsoleHelper consoleHelper;

		private MultiValueDictionary multiValueDictionary = new MultiValueDictionary();

		public CommandRunner()
			: this(new CommandFactory(), new ConsoleHelper())
		{
		}

		public CommandRunner(ICommandFactory commandFactory, IConsoleHelper consoleHelper)
		{
			this.commandFactory = commandFactory;
			this.consoleHelper = consoleHelper;
		}

		public void Run(CommandType commandType, string commandText)
		{
			var command = commandFactory.Get(commandType, commandText);

			if (!command.IsValid(out var errorMessage))
			{
				consoleHelper.WriteError(errorMessage);
				return;
			}

			command.Execute(multiValueDictionary);
		}
	}
}
