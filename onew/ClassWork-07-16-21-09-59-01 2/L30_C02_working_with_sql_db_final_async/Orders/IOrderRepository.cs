using System.Threading.Tasks;

namespace L30_C02_working_with_sql_db_final_async.Orders
{
	public interface IOrderRepository
	{
		Task<Order> GetByIdAsync(int id);
		Task<Order[]> GetAllAsync();
		Task<int> GetCountAsync();
		Task<int> Insert(InsertOrderCommand dto);
	}
}