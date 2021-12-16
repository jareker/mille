using System;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mille.Controllers;
using Mille.Domain.Aggregates;
using Moq;
using TestProject1.Stubs;
using Xunit;

namespace TestProject1
{
	public class ResourceControllerTests
	{
		[Fact]
		public async void GetResource_ReturnsOkWithResource()
		{
			var repositoryStub = new RepositoryStub();
			var logger = Mock.Of<ILogger<ResourceController>>();
			var controller = new ResourceController(repositoryStub, logger);

			var resource = new Resource("testResource");
			
			await repositoryStub.Create(resource);
			var result = await controller.Get();
			Assert.True(result is OkObjectResult);
		}
		
		// time is up.
	}
}