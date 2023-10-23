using GraphLib.Operations;
using Microsoft.Extensions.Configuration;

namespace GraphExplorer.Console
{
	class Program
	{
		protected Program() { }
		
		static async Task Main(string[] args)
		{
			LogMessage("System Message - Console App Started!");
			IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddUserSecrets<Program>().Build();

			UserData userOperations = new UserData(config);
			var userResult = await userOperations.GetUsersAsync();
			if (userResult != null && userResult.Any())
			{
				LogMessage($"User: Id\t\tDisplayName\t\tUserPrincipalName", true);
				foreach (var user in userResult)
				{
					LogMessage($"User: {user.Id}\t\t{user.DisplayName}\t\t{user.UserPrincipalName}", true);
				}
			}

			GroupData groupOperations = new GroupData(config);
			var groupResult = await groupOperations.GetGroupsAsync();
			if (groupResult != null && groupResult.Any())
			{
				LogMessage($"Group: Id\t\tDisplayName\t\tSecurityIdentifier\t\tMailEnabled\t\tSecurityEnabled", true);
				foreach (var group in groupResult)
				{
					LogMessage($"Group: {group.Id}\t\t{group.DisplayName}\t\t{group.SecurityIdentifier}\t\t{group.MailEnabled}\t\t{group.SecurityEnabled}", true);
				}
			}
			LogMessage("System Message - Console App Completed!");
		}
		static void LogMessage(string message, bool noDate = false)
		{
			if(noDate)
			{
				System.Console.WriteLine(message);
			}
			else
			{
				var msg = $"UTC:{DateTime.UtcNow} => {message}";
				System.Console.WriteLine(msg);
			}
			
		}
	}
}