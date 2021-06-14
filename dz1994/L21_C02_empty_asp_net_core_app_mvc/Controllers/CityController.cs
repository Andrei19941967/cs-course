using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using L21_C02_empty_asp_net_core_app_mvc.Model;
using L21_C02_empty_asp_net_core_app_mvc.ViewModels;

namespace L21_C02_empty_asp_net_core_app_mvc.Controllers
{
	// 
	// RestFul API
	// TODO: (CRUD)
	// 1. Получить список всех городов (Read)
	//    GET /api/cities
	// 2. Получить город по идентификатору (Read)
	//    GET /api/cities/{id}
	// 3. Создать город (Create)
	//    POST /api/cities/{id ?} 
	// 4. Изменить информацию о городе (Update)
	//    PATCH /api/cities/{id}
	// 5. Удалить город (Delete)
	//    DELETE /api/cities/{id}
	// JSON
	// JavaScript Object Notation
	// XML

	[Route("/api/cities")]
	public class CityController : ControllerBase
	{
		private readonly ICityStorage _storage;

		public CityController(ICityStorage storage)
		{
			_storage = storage;
		}

		[HttpGet]
		public IActionResult Get()
		{

			var cities = _storage.GetAll();

			return Ok(cities);
		}

		[HttpGet("{id:guid}")]
		public IActionResult Get(Guid id)
		{
			var found = _storage.Find(id);

			if (found is null)
			{
				return NotFound();
			}

			return Ok(found);
		}

		[HttpPost("{id:guid}")]
		public IActionResult Create(Guid? id, [FromBody] CityCreateViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var city = new City(
				id ?? Guid.NewGuid(),
				viewModel.Name.Trim(),
				viewModel.Description.Trim(),
				viewModel.Population
			);

			var duplicate1 = _storage.Find(city.Id);
			var duplicate2 = _storage.Find(city.Name);
			if (duplicate1 is not null || duplicate2 is not null)
			{
				ModelState.AddModelError("Id", "The city with specified Id or Name already exists");
				ModelState.AddModelError("Name", "The city with specified Id or Name already exists");

				return Conflict(ModelState);
			}

			_storage.Create(city);

			return CreatedAtAction("Get", new {id = id.Value}, city);
		}

		[HttpDelete("{id:guid}")]
		public IActionResult Delete(Guid id)
		{
			City find = _storage.Find(id);
			if (find is null) return NotFound();
			
			_storage.Delete(id);

			return Ok();
		}
		
		[HttpPut("{id:guid}")]
		public IActionResult Update(Guid id, [FromBody] CityCreateViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var city = new City(
				id,
				viewModel.Name.Trim(),
				viewModel.Description.Trim(),
				viewModel.Population
			);
			
			var duplicate = _storage.Find(city.Id);
			if (duplicate is  null )
			{
				ModelState.AddModelError("Id", "doesn't exists");

				return Conflict(ModelState);
			}
			
			_storage.Update(id, city);

			return Ok();
		}
	}
}