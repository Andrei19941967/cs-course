using System.Threading.Tasks;

namespace L30_C02_working_with_sql_db_final_async.Products
{
	public interface IProductRepository
	{
		Task<Product> GetByIdAsync(int id);
		Task<Product[]> GetAllAsync();
		Task<int> GetCountAsync();
		Task<int> Insert(InsertProductCommand command);
	}
}