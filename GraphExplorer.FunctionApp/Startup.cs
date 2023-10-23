using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

[assembly: FunctionsStartup(typeof(GraphExplorer.FunctionApp.Functions.Startup))]

namespace GraphExplorer.FunctionApp.Functions
{
    public class Startup : FunctionsStartup
    {
		public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
		{
			FunctionsHostBuilderContext context = builder.GetContext();

			builder.ConfigurationBuilder
				.AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
				.AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
				.AddJsonFile(Path.Combine(context.ApplicationRootPath, $"local.settings.json"), optional: true, reloadOnChange: false)
				.AddEnvironmentVariables();
		}

		public override void Configure(IFunctionsHostBuilder builder)
        {
			builder.Services.AddLogging();
		}
    }
}
