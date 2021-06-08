using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;



namespace L21_C02_empty_asp_net_core_app_mvc.Controllers
{
	[Route("/api/cities")]
	public class CityController : ControllerBase
	{
		private static List<string> cities = new List<string>();
		
		[HttpGet("{name}")]
		public IActionResult Get(string name)
		{
			for(int i = 0; i < cities.Count; i++)
				if (cities[i] == name)
					return Ok(name);
			
			return Ok("not found");
		}

		[HttpPost("create/{name}")]
		public IActionResult Post(string name)
		{
			cities.Add(name);
			return Ok("Created");
		}
		
		[HttpPut("{name}/change/{newName}")]
		public IActionResult Put(string name, string newName)
		{
			for (int i = 0; i < cities.Count; i++)
			{
				if (cities[i] == name)
				{
					cities[i] = newName;
					return Ok("Changed");
				}
			}

			return Ok("not found");
		}

		[HttpDelete("delete/{name}")]
		public IActionResult Delete(string name)
		{
			for (int i = 0; i < cities.Count; i++)
			{
				if (cities[i] == name)
				{
					cities.RemoveAt(i);
					return Ok("deleted");
				}
			}

			return Ok("not found");
		}
		
	}
}