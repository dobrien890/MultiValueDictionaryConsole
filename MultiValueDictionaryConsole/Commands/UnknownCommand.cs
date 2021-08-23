using System;

namespace MultiValueDictionaryConsole.Commands
{
	internal class UnknownCommand : ICommand
	{
		private readonly string commandText;

		public UnknownCommand(string commandText)
		{
			this.commandText = commandText;
		}

		public void Execute(MultiValueDictionary multiValueDictionary)
		{
			throw new NotSupportedException("Cannot execute unknown command");
		}

		public bool IsValid(out string errorMessage)
		{
			errorMessage = $"Unknown command [{commandText.Split(' ')[0]}] provided";
			return false;
		}
	}
}
