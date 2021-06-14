using System;

namespace L21_C02_empty_asp_net_core_app_mvc.Model
{
	public class City
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Population { get; set; }

		public City()
		{
		}

		public City(Guid id, string name, string description, int population)
		{
			Id = id;
			Name = name;
			Description = description;
			Population = population;
		}
	}
}