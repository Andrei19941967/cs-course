namespace L30_C02_working_with_sql_db_final_async.Products
{
	public class InsertProductCommand
	{
		public string Name { get; }
		public decimal Price { get; }

		public InsertProductCommand(string name, decimal price)
		{
			Name = name;
			Price = price;
		}
	}
}