using System;
using System.Collections.Generic;
using L21_C02_empty_asp_net_core_app_mvc.ViewModels;

namespace L21_C02_empty_asp_net_core_app_mvc.Model
{
	public interface ICityStorage
	{
		public City Find(Guid id);
		public City Find(string name);

		public CityListViewModel[] GetAll();
		public void Create(City c);
		public void Update(Guid id, City c);
		public void Delete(Guid id);



	}
}