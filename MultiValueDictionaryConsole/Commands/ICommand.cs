namespace MultiValueDictionaryConsole.Commands
{
	internal interface ICommand
	{
		void Execute(MultiValueDictionary multiValueDictionary);
		bool IsValid(out string errorMessage);
	}
}
