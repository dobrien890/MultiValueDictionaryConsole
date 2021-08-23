using MultiValueDictionaryConsole.Helpers;

namespace MultiValueDictionaryConsole
{
	internal class CommandRunner : ICommandRunner
	{
		private readonly ICommandTypeProvider commandTypeProvider;
		private readonly ICommandFactory commandFactory;
		private readonly IConsoleHelper consoleHelper;

		private MultiValueDictionary multiValueDictionary = new MultiValueDictionary();

		public CommandRunner()
			: this(new CommandTypeProvider(), new CommandFactory(), new ConsoleHelper())
		{
		}

		public CommandRunner(
			ICommandTypeProvider commandTypeProvider,
			ICommandFactory commandFactory,
			IConsoleHelper consoleHelper)
		{
			this.commandTypeProvider = commandTypeProvider;
			this.commandFactory = commandFactory;
			this.consoleHelper = consoleHelper;
		}

		public void Run(string commandText)
		{
			var commandType = commandTypeProvider.Get(commandText);
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
