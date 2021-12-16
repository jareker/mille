using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mille.Infrastructure.Context;

namespace Mille
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			
			CreateDatabase(host);
			
			host.Run();
		}

		private static void CreateDatabase(IHost host)
		{
			using var scope = host.Services.CreateScope();

			var services = scope.ServiceProvider;

			var dbContext = services.GetRequiredService<ApplicationDbContext>();
			// we use in memory database, so there is no migration.
			dbContext.Database.EnsureCreated();
		}
		
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}