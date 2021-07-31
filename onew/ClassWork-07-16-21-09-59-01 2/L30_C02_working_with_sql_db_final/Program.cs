using System;
using L30_C02_working_with_sql_db_final.Orders;
using L30_C02_working_with_sql_db_final.Products;

namespace L30_C02_working_with_sql_db_final
{
	internal class Program
	{
		private const string ConnectionString =
			"Server=tcp:camelot-sql-server.database.windows.net,1433;" +
			"Initial Catalog=reminder; " +
			"Persist Security Info=False;" +
			"User ID=a_orlov@camelot-sql-server;" +
			"Password=E4fy3ES1qVxEI6ld13Uw;" +
			"Encrypt=True;";

		private static void Main(string[] args)
		{
			Console.WriteLine("Working with products");

			var productRepository = new ProductRepository(ConnectionString);

			Console.WriteLine($"Count of products: {productRepository.GetCount()}");
			foreach (var product in productRepository.GetAll())
			{
				Console.WriteLine($"({product.Id}) {product.Name} for {product.Price}");
			}
			var createdProductId = productRepository.Insert(
				new CreateProductCommand("SuperWatch Series 500", 20000)
			);
			var createdProduct = productRepository.GetById(createdProductId);
			Console.WriteLine(
				$"Created product \"{createdProduct.Name}\" " +
				$"For {createdProduct.Price} ({createdProduct.Id})"
			);

			Console.WriteLine("Working with orders");

			var orderRepository = new OrderRepository(ConnectionString);

			Console.WriteLine($"Count of orders: {orderRepository.GetCount()}");
			foreach (var order in orderRepository.GetAll())
			{
				Console.WriteLine(
					$"Accepted order ({order.Id}) " +
					$"From customer \"{order.Customer}\" " +
					$"At {order.OrderDate} " +
					$"With total revenue: {order.Total} ({order.Discount * 100} %)"
				);
			}

			var createdOrderId = orderRepository.Insert(
				new CreateOrderCommand(
					customerId: 4,
					orderDate: DateTimeOffset.Now,
					discount: 0.15m,
					new CreateOrderCommand.Line(createdProductId, 2),
					new CreateOrderCommand.Line(1, 2)
				)
			);
			var createdOrder = orderRepository.GetById(createdOrderId);
			Console.WriteLine(
				$"Accepted order ({createdOrder.Id}) " +
				$"From customer \"{createdOrder.Customer}\" " +
				$"At {createdOrder.OrderDate} " +
				$"With total revenue: {createdOrder.Total} ({createdOrder.Discount * 100} %)"
			);
			
			
		}
	}
}