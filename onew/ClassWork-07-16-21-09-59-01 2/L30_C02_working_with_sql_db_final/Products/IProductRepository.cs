namespace L30_C02_working_with_sql_db_final.Products
{
	public interface IProductRepository
	{
		int GetCount();

		Product GetById(int id);

		Product[] GetAll();

		int Insert(CreateProductCommand command);

		void Delete(int id);

		void Update(int id, CreateProductCommand command);
	}
}