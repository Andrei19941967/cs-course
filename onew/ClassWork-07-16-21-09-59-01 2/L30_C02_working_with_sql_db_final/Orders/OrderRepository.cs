using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace L30_C02_working_with_sql_db_final.Orders
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

		public OrderRepository(string connectionString) : base(connectionString)
		{
		}

		public int GetCount()
		{
			using var connection = CreateConnection();
			using var command = connection.CreateQuery("SELECT COUNT([Id]) FROM [Order]");

			return (int) command.ExecuteScalar();
		}

		public Order GetById(int id)
		{
			using var connection = CreateConnection();
			using var command = connection.CreateQuery($"{OrderQuery}\nWHERE O.Id = @id");

			command.Parameters.AddWithValue("@id", id);

			using var reader = command.ExecuteReader();

			if (!reader.Read())
			{
				throw new ArgumentException(
					"Invalid order id specified"
				);
			}

			return new Order(
				reader.GetInt32("Id"),
				reader.GetString("Customer"),
				reader.GetDateTimeOffset(reader.GetOrdinal("OrderDate")),
				reader.IsDBNull("Discount")
					? 0m
					: (decimal) reader.GetDouble("Discount"), 
				reader.GetDecimal("Total")
			);
		}

		public Order[] GetAll()
		{
			using var connection = CreateConnection();
			using var command = connection.CreateQuery($"{OrderQuery}\nORDER BY O.[OrderDate] DESC");
			using var reader = command.ExecuteReader();

			if (!reader.HasRows)
			{
				return Array.Empty<Order>();
			}

			var result = new List<Order>();
			var idIndex = reader.GetOrdinal("Id");
			var customerIndex = reader.GetOrdinal("Customer");
			var orderDateIndex = reader.GetOrdinal("OrderDate");
			var discountIndex = reader.GetOrdinal("Discount");
			var totalIndex = reader.GetOrdinal("Total");

			while (reader.Read())
			{
				result.Add(
					new Order(
						reader.GetInt32(idIndex),
						reader.GetString(customerIndex),
						reader.GetDateTimeOffset(orderDateIndex),
						reader.IsDBNull(discountIndex)
							? 0m
							: (decimal) reader.GetDouble(discountIndex), 
						reader.GetDecimal(totalIndex)
					)
				);
			}

			return result.ToArray();
		}

		public int Insert(CreateOrderCommand dto)
		{
			using var connection = CreateConnection();
			using var transaction = connection.BeginTransaction();

			try
			{
				using var orderCommand = connection.CreateProcedure("sp_AddOrder", transaction);
				orderCommand.Parameters.AddWithValue("@p_customerId", dto.CustomerId);
				orderCommand.Parameters.AddWithValue("@p_orderDate", dto.OrderDate);
				if (dto.Discount != 0m)
				{
					orderCommand.Parameters.AddWithValue("@p_discount", dto.Discount);
				}
				var orderId = orderCommand.Parameters.AddOutput("@p_id", SqlDbType.Int);
				orderCommand.ExecuteNonQuery();

				using var orderLineCommand = connection.CreateProcedure("sp_AddOrderLine", transaction);

				foreach (var line in dto.OrderLines)
				{
					orderLineCommand.Parameters.Clear();
					orderLineCommand.Parameters.AddWithValue("@p_orderId", (int) orderId.Value);
					orderLineCommand.Parameters.AddWithValue("@p_productId", line.ProductId);
					orderLineCommand.Parameters.AddWithValue("@p_count", line.Count);
					orderLineCommand.ExecuteNonQuery();
				}

				transaction.Commit();

				return (int) orderId.Value;
			}
			catch (SqlException)
			{
				transaction.Rollback();
				throw;
			}
		}

		public void Delete(int id)
		{
			using var connection = CreateConnection();
			using var commandDeleteOrderLines = connection.CreateProcedure("sp_DeleteOrderLines");

			commandDeleteOrderLines.Parameters.AddWithValue("@p_orderId", id);
			commandDeleteOrderLines.ExecuteNonQuery();

			using var commandDeleteOrder = connection.CreateProcedure("sp_DeleteOrder");
			commandDeleteOrder.Parameters.AddWithValue("@p_id", id);
			commandDeleteOrder.ExecuteNonQuery();
			
		}
	}
}