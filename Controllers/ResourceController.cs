using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mille.Commands;
using Mille.Domain.Aggregates;
using Mille.Mappings;
using Newtonsoft.Json;

namespace Mille.Controllers
{
	[ApiController]
	[Route("/v1/[controller]")]
	public class ResourceController : ControllerBase
	{
		private readonly IResourceRepository _resourceRepository;
		private readonly ILogger<ResourceController> _logger;

		public ResourceController(IResourceRepository resourceRepository, ILogger<ResourceController> logger)
		{
			_resourceRepository = resourceRepository;
			_logger = logger;
		}
		
		[HttpGet()]
		public async Task<IActionResult> Get()
		{
			_logger.LogInformation($"Get all resources request");
			var resources = await _resourceRepository.GetAll();

			return Ok(resources);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			_logger.LogInformation($"Get request from: {this.HttpContext.Connection.RemoteIpAddress}");
			_logger.LogInformation($"Resource id: {id}");
			var resource = await _resourceRepository.GetById(id);

			if (resource != null)
			{
				_logger.LogInformation($"Handled GET request.");
				return Ok(resource);
			}

			return NotFound();
		}

		[HttpPost]
		public IActionResult Post([FromBody] CreateResourceCommand command)
		{

			if (!ModelState.IsValid)
			{
				_logger.LogWarning($"Invalid model state. Command: {JsonConvert.SerializeObject(command)}");
				return BadRequest("Invalid model state");
			}
			
			var resource = new Resource(command.Name);
			
			foreach (var modelItem in command.Items)
			{
				var resourceItem = ResourceItemMapper.FromDataModel(modelItem);
				resource.AddItem(resourceItem);
			}
			try
			{
				_logger.LogInformation($"Trying to insert resource to database.");
				_resourceRepository.Create(resource);
				_resourceRepository.UnitOfWork.SaveChangesAsync();
			}
			catch (Exception e)
			{
				_logger.LogError($"An error occurred during database insert. \n Resource: {JsonConvert.SerializeObject(resource)}", e);
				BadRequest();
			}

			_logger.LogInformation($"Resource created with id: {resource.Id}");
			return Created($"/v1/{resource.Id}", command);

		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] UpdateResourceCommand command)
		{
			var existingResource = await _resourceRepository.GetById(command.Id);
			if (existingResource == null)
			{
				_logger.LogInformation($"Resource with id: {command.Id} not found");
				return NotFound();
			}

			try
			{
				_logger.LogInformation($"Try to change resource name from: {existingResource.Name} to {command.Name}");
				existingResource.ChangeResourceName(command.Name);
			}
			catch (Exception e)
			{
				_logger.LogError($"Error during changing resource name", e);
				return BadRequest();
			}
			
			_logger.LogInformation($"Resource name successfully changed from: {existingResource.Name} to {command.Name}");
			
			existingResource.UpdateItems(command.Items.Select(ResourceItemMapper.FromDataModel));

			try
			{
				await _resourceRepository.Update(existingResource);
				await _resourceRepository.UnitOfWork.SaveChangesAsync();

			}
			catch (Exception e)
			{
				return BadRequest();
			}
			_logger.LogInformation($"Resource successfully updated");
			return Ok(existingResource);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromQuery] Guid resourceId)
		{
			var existingResource = await _resourceRepository.GetById(resourceId);
			if (existingResource == null)
			{
				_logger.LogInformation($"Resource with id: {resourceId} not found");
				return NotFound();
			}

			await _resourceRepository.Delete(existingResource);

			_logger.LogInformation($"Resource with id: {resourceId} deleted.");
			
			return Ok(existingResource);
		}
		
	}
}