using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using GraphLib;

namespace GraphLib.Operations
{
	public class UserData : ClientBase
	{
		private GraphServiceClient? _client;
		private bool _initialized;

		public UserData(IConfiguration config) : base(config)
		{
			_initialized = false;
			_client = null;
			Initialize();
		}

		private void Initialize()
		{
			_client = GetClient();
			_initialized = true;
		}

		public async Task<List<User>?> GetUsersAsync()
		{
			if( !_initialized || _client is null )
			{
				NullReferenceException ex = new NullReferenceException("The graph client was not initialized.");
				throw ex;
			}
			UserCollectionResponse? response = await _client.Users.GetAsync(requestConfiguration =>
			{
				requestConfiguration.QueryParameters.Top = 10;
				requestConfiguration.QueryParameters.Select = new string[] { "id", "userPrincipalName", "displayName", "givenName", "surname", "city", "mailNickname", "userType" };
				requestConfiguration.QueryParameters.Orderby = new string[] { "displayName" };
			});
			if (response != null)
			{
				return response.Value;
			}
			else
			{
				return null;
			}		
		}

		public async Task<User?> GetUserByIdAsync(string id)
		{
			if(string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("Parameter for the query was invalid.", nameof(id));
			}
			if (!_initialized || _client is null)
			{
				NullReferenceException ex = new NullReferenceException("The graph client was not initialized.");
				throw ex;
			}
			var response = await _client.Users[id].GetAsync(requestConfiguration =>
			{
				requestConfiguration.QueryParameters.Select = new string[] { "id", "userPrincipalName", "displayName", "givenName", "surname", "city", "mailNickname", "userType" };
			});
			return response;
		}

		public async Task<User?> GetUserByUpnAsync(string userPrincipalName)
		{
			if (string.IsNullOrEmpty(userPrincipalName))
			{
				throw new ArgumentException("Parameter for the query was invalid.", nameof(userPrincipalName));
			}
			if (!_initialized || _client is null)
			{
				NullReferenceException ex = new NullReferenceException("The graph client was not initialized.");
				throw ex;
			}
			var response = await _client.Users[userPrincipalName].GetAsync(requestConfiguration =>
			{
				requestConfiguration.QueryParameters.Select = new string[] { "id", "userPrincipalName", "displayName", "givenName", "surname", "city", "mailNickname", "userType" };
			});
			return response;
		}
	}
}
