namespace MultiValueDictionaryConsole.Helpers
{
	internal static class ArgumentHelper
	{
		public static string GetKey(string commandText)
		{
			var parameters = commandText.Split(' ');
			var key = parameters[1];
			return key;
		}

		public static (string key, string member) GetKeyAndMember(string commandText)
		{
			var parameters = commandText.Split(' ');
			var key = parameters[1];
			var member = parameters[2];
			return (key, member);
		}
	}
}
