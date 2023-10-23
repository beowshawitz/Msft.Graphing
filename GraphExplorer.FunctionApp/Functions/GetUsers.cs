using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GraphLib.Operations;
using Microsoft.Extensions.Configuration;

namespace GraphExplorer.FunctionApp.Functions
{
    public class GetUsers
    {
		private readonly IConfiguration _configuration;

		public GetUsers(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[FunctionName("GetUsers")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ExecutionContext executionContext, ILogger log)
        {
            log.LogInformation($"An HTTP {req.Method} request was triggered on {executionContext.FunctionName}.");

            try
            {
				UserData userOps = new UserData(_configuration);
				var users = await userOps.GetUsersAsync();
				if(users != null)
				{
					return new OkObjectResult(JsonConvert.SerializeObject(users));
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
