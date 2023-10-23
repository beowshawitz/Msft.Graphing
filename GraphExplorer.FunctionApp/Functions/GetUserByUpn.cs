using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using GraphLib.Operations;

namespace GraphExplorer.FunctionApp
{
    public class GetUserByUpn
	{
		private readonly IConfiguration _configuration;

		public GetUserByUpn(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[FunctionName("GetUserByUpn")]
		public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ExecutionContext executionContext, ILogger log)
		{
			log.LogInformation($"An HTTP {req.Method} request was triggered on {executionContext.FunctionName}.");

			string id = req.Query["id"];

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			id = id ?? data?.id;
			if(string.IsNullOrWhiteSpace(id))
			{
				return new BadRequestObjectResult("An identifier was missing from the request query string or body.");
			}

			try
			{
				UserData userOps = new UserData(_configuration);
				var user = await userOps.GetUserByUpnAsync(id);
				if (user != null)
				{
					return new OkObjectResult(JsonConvert.SerializeObject(user));
				}
				else
				{
					return new OkObjectResult(new EmptyResult());
				}
			}
			catch (Exception ex)
			{
				return new BadRequestObjectResult(ex);
			}
		}
	}
}
