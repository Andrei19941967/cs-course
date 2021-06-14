using System;
using L21_C02_empty_asp_net_core_app_mvc.Model;

namespace L21_C02_empty_asp_net_core_app_mvc.ViewModels
{
	public class CityListViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public CityListViewModel(City city)
		{
			Id = city.Id;
			Name = city.Name;
		}
	}
}