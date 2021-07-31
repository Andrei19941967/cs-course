namespace L30_C02_working_with_sql_db_final.Products
{
	public class CreateProductCommand
	{
		public string Name { get; set; }
		public decimal Price { get; set; }

		public CreateProductCommand(string name, decimal price)
		{
			Name = name;
			Price = price;
		}
	}
}