using System;
using System.Collections.Generic;
using System.Linq;
using L21_C02_empty_asp_net_core_app_mvc.ViewModels;

namespace L21_C02_empty_asp_net_core_app_mvc.Model
{
	public class CityStorage : ICityStorage
	{
		public List<City> Cities { get; set; } =
			new List<City>
			{
				new City(Guid.NewGuid(), "Moscow", "The capital of Russia", 15_000_000),
				new City(Guid.NewGuid(), "Saint Petersburg", "The second capital of Russia", 5_000_000),
			};

		// Singleton
		public CityStorage()
		{
		}

		private static CityStorage _instance;

		public static CityStorage Instance =>
			_instance ??= new CityStorage();

		public City Find(Guid id)
		{
			var found = Cities
				.FirstOrDefault(city => city.Id == id);

			return found;
		}

		public City Find(string name)
		{
			var found = Cities
				.FirstOrDefault(city => city.Name == name);

			return found;
		}

		public CityListViewModel[] GetAll()
		{
			var cities = 
				 Cities
				.Select(city => new CityListViewModel(city))
				.OrderBy(city => city.Name)
				.ToArray();

			return cities;
		}

		public void Create(City c)
		{
			Cities.Add(c);
		}

		public void Update(Guid id, City c)
		{
			for (int i = 0; i < Cities.Count; i++)
			{
				if (Cities[i].Id == id)
				{
					Cities[i] = c;
					break;
					
				}
			}
		}

		public void Delete(Guid id)
		{
			for (int i = 0; i < Cities.Count; i++)
			{
				if (Cities[i].Id == id)
				{
					Cities.Remove(Cities[i]);
					break;
					
				}
			}
		}
	}
}