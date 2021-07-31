using System;
using System.Threading.Tasks;
using L30_C02_working_with_sql_db_final_async.Orders;
using L30_C02_working_with_sql_db_final_async.Products;

namespace L30_C02_working_with_sql_db_final_async
{
	class Program
	{
		private const string ConnectionString =
			"Server=tcp:camelot-sql-server.database.windows.net,1433;" +
			"Initial Catalog=reminder; " +
			"Persist Security Info=False;" +
			"User ID=<username>@camelot-sql-server;" +
			"Password=<password>;" +
			"Encrypt=True;";

		private static readonly ProductRepository Products = new ProductRepository(ConnectionString);
		private static readonly OrderRepository Orders = new OrderRepository(ConnectionString);

		static async Task Main(string[] args)
		{
			Console.WriteLine("----Products----");
			Console.WriteLine($"Total products: {await Products.GetCountAsync()}");
			Console.WriteLine("Read all Products");
			foreach (var product in await Products.GetAllAsync())
			{
				Console.WriteLine(product);
			}

			Console.WriteLine("Insert concrete product");
			var productId = await Products.Insert(
				new InsertProductCommand("Apple Watch 6", 25_000m)
			);
			Console.WriteLine("Read inserted product by id");
			Console.WriteLine(await Products.GetByIdAsync(productId));

			Console.WriteLine("----Orders----");
			Console.WriteLine($"Total orders: {await Orders.GetCountAsync()}");
			Console.WriteLine("Read all Products");
			foreach (var order in await Orders.GetAllAsync())
			{
				Console.WriteLine(order);
			}

			Console.WriteLine("Insert concrete order");
			var orderId = await Orders.Insert(
				new InsertOrderCommand(customerId: 3)
				{
					OrderLines = {(1, 3), (2, 5)}
				}
			);
			Console.WriteLine("Read inserted order by id");
			Console.WriteLine(await Orders.GetByIdAsync(orderId));
		}
	}
}