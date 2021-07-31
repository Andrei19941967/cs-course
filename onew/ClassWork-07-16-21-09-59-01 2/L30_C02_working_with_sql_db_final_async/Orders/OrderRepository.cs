using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using L30_C02_working_with_sql_db_final_async.Products;
using Microsoft.Data.SqlClient;

namespace L30_C02_working_with_sql_db_final_async.Orders
{
	public class OrderRepository : BaseRepository, IOrderRepository
	{
		private const string OrderQuery = @"
SELECT 
  O.Id AS Id,
  C.Name AS Customer,
  O.OrderDate AS OrderDate,
  O.Discount AS Discount,
  OC.Total AS Total
FROM (
  SELECT 
    O.[Id],
    CAST((1 - ISNULL(O.[Discount], 0)) AS MONEY) * SUM(OL.[Count] * P.[Price]) AS Total
    FROM [Order] O
    JOIN [OrderLine] OL 
      ON O.Id = OL.OrderId
    JOIN [Product] P
      ON P.Id = OL.ProductId
    GROUP BY O.[Id], O.[Discount]
) AS OC
JOIN [Order] O
  ON O.Id = OC.Id
JOIN [Customer] C
  ON C.Id = O.CustomerId";

		public OrderRepository(string connection) : base(connection)
		{
		}

		public async Task<int> GetCountAsync()
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateQuery("SELECT COUNT([Id]) FROM [Order]");

			return (int) await command.ExecuteScalarAsync();
		}

		public async Task<Order> GetByIdAsync(int id)
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateQuery($"{OrderQuery}\nWHERE O.Id = @id");

			command.Parameters.AddWithValue("@id", id);

			await using var reader = await command.ExecuteReaderAsync();
			var order = await ReadOrdersAsync(reader).FirstOrDefaultAsync();
			if (order is null)
			{
				throw new ArgumentException($"Order with id {id} not found");
			}

			return order;
		}

		public async Task<Order[]> GetAllAsync()
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateQuery(OrderQuery);
			await using var reader = await command.ExecuteReaderAsync();

			return await ReadOrdersAsync(reader).ToArrayAsync();
		}

		public async Task<int> Insert(InsertOrderCommand dto)
		{
			await using var connection = await GetConnection();
			await using var transaction = (SqlTransaction) await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
			await using var command = connection.CreateProcedure("InsertOrder", transaction);
			command.Parameters.AddWithValue("p_customerId", dto.CustomerId);
			command.Parameters.AddWithValue("p_orderDate", dto.OrderDate);
			command.Parameters.AddWithValue("p_discount", dto.Discount.HasValue ? (object) dto.Discount.Value : DBNull.Value);
			var orderId = command.Parameters.AddOutput("p_id", SqlDbType.Int);

			try
			{
				await command.ExecuteNonQueryAsync();

				foreach (var (productId, count) in dto.OrderLines)
				{
					await using var lineCommand = connection.CreateQuery("INSERT INTO [OrderLine] (OrderId, ProductId, Count) VALUES(@orderId, @productId, @count)", transaction);
					lineCommand.Parameters.AddWithValue("orderId", (int) orderId.Value);
					lineCommand.Parameters.AddWithValue("productId", productId);
					lineCommand.Parameters.AddWithValue("count", count);

					await lineCommand.ExecuteNonQueryAsync();
				}

				await transaction.CommitAsync();
			}
			catch (SqlException)
			{
				await transaction.RollbackAsync();
				throw;
			}

			return (int) orderId.Value;
		}

		private static async IAsyncEnumerable<Order> ReadOrdersAsync(SqlDataReader reader)
		{
			if (!reader.HasRows)
			{
				yield break;
			}

			var id = reader.GetOrdinal("Id");
			var customer = reader.GetOrdinal("Customer");
			var orderDate = reader.GetOrdinal("OrderDate");
			var discount = reader.GetOrdinal("Discount");
			var total = reader.GetOrdinal("Total");

			while (await reader.ReadAsync())
			{
				var product = new Order(
					reader.GetInt32(id),
					reader.GetString(customer),
					reader.GetDateTimeOffset(orderDate),
					reader.IsDBNull(discount)
						? default
						: (decimal) reader.GetDouble(discount),
					reader.GetDecimal(total)
				);
				yield return product;
			}
		}
	}
}