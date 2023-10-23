using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using GraphLib;

namespace GraphLib.Operations
{
	public class GroupData : ClientBase
	{
		private GraphServiceClient? _client;
		private bool _initialized;

		public GroupData(IConfiguration config) : base(config)
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

		public async Task<List<Group>?> GetGroupsAsync()
		{
			if( !_initialized || _client is null )
			{
				NullReferenceException ex = new NullReferenceException("The graph client was not initialized.");
				throw ex;
			}
			GroupCollectionResponse? response = await _client.Groups.GetAsync(requestConfiguration =>
			{
				requestConfiguration.QueryParameters.Top = 10;
				requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName", "securityIdentifier", "securityEnabled", "mailEnabled", "mail", "description" };
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

		public async Task<List<Group>?> GetSecurityGroupsAsync()
		{
			if (!_initialized || _client is null)
			{
				NullReferenceException ex = new NullReferenceException("The graph client was not initialized.");
				throw ex;
			}
			GroupCollectionResponse? response = await _client.Groups.GetAsync(requestConfiguration =>
			{
				requestConfiguration.QueryParameters.Top = 10;
				requestConfiguration.QueryParameters.Filter = "securityEnabled eq true";
				requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName", "securityIdentifier", "securityEnabled", "mailEnabled", "mail", "description" };
				requestConfiguration.QueryParameters.Orderby = new string[] { "displayName" };
				//The next two settings are required for advanced queries, because filter and orderby in the same request are considered advanced.
				requestConfiguration.QueryParameters.Count = true;
				requestConfiguration.Headers.Add("ConsistencyLevel", "eventual");
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

		public async Task<List<Group>?> GetMailGroupsAsync()
		{
			if (!_initialized || _client is null)
			{
				NullReferenceException ex = new NullReferenceException("The graph client was not initialized.");
				throw ex;
			}
			GroupCollectionResponse? response = await _client.Groups.GetAsync(requestConfiguration =>
			{
				requestConfiguration.QueryParameters.Top = 10;
				requestConfiguration.QueryParameters.Filter = "mailEnabled eq true";
				requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName", "securityIdentifier", "securityEnabled", "mailEnabled", "mail", "description" };
				requestConfiguration.QueryParameters.Orderby = new string[] { "displayName" };
				//The next two settings are required for advanced queries, because filter and orderby in the same request are considered advanced.
				requestConfiguration.QueryParameters.Count = true;
				requestConfiguration.Headers.Add("ConsistencyLevel", "eventual");
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

		public async Task<Group?> GetGroupByIdAsync(string id)
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
			var response = await _client.Groups[id].GetAsync(requestConfiguration =>
			{
				requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName", "securityIdentifier", "securityEnabled", "mailEnabled", "mail", "description" };
			});
			return response;
		}

		
	}
}
