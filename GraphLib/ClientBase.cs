using GraphLib.TokenProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;

namespace GraphLib
{
	public class ClientBase
	{
		private readonly IConfiguration _configuration;
		public ClientBase(IConfiguration config)
		{
			_configuration = config;
		}

		protected GraphServiceClient GetClient()
		{
			var authenticationProvider = new BaseBearerTokenAuthenticationProvider(new ConfidentialClientTokenProvider(_configuration));
			return new GraphServiceClient(authenticationProvider);
		}
	}
}
