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
    public class GetMailGroups
	{
		private readonly IConfiguration _configuration;

		public GetMailGroups(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[FunctionName("GetMailGroups")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ExecutionContext executionContext, ILogger log)
        {
			log.LogInformation($"An HTTP {req.Method} request was triggered on {executionContext.FunctionName}.");

			try
            {
				GroupData groupOps = new GroupData(_configuration);
				var groups = await groupOps.GetMailGroupsAsync();
				if(groups != null)
				{
					return new OkObjectResult(JsonConvert.SerializeObject(groups));
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
