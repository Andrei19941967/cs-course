using System.Collections;
using System.Collections.Generic;

namespace L30_C02_working_with_sql_db_final.Orders
{
	public interface IOrderRepository
	{
		int GetCount();
		Order GetById(int id);
		Order[] GetAll();
		int Insert(CreateOrderCommand command);

		void Delete(int id);
		
		

	}
}