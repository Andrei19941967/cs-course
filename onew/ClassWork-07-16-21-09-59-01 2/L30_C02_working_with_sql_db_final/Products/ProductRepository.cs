using System;
using System.Collections.Generic;
using System.Data;

namespace L30_C02_working_with_sql_db_final.Products
{
	public class ProductRepository : BaseRepository, IProductRepository
	{
		public ProductRepository(string connectionString) : base(connectionString)
		{
		}

		public int GetCount()
		{
			using var connection = CreateConnection();
			using var command = connection.CreateQuery("SELECT COUNT([Id]) FROM [Product]");

			return (int) command.ExecuteScalar();
		}

		public Product GetById(int id)
		{
			using var connection = CreateConnection();
			using var command = connection.CreateQuery("SELECT [Id],[Name],[Price] FROM [Product] WHERE [Id] = @id");

			command.Parameters.AddWithValue("@id", id);

			using var reader = command.ExecuteReader();

			if (!reader.Read())
			{
				throw new ArgumentException(
					"Invalid order id specified"
				);
			}

			return new Product(
				reader.GetInt32("Id"),
				reader.GetString("Name"),
				reader.GetDecimal("Price")
			);
		}

		public Product[] GetAll()
		{
			using var connection = CreateConnection();
			using var command = connection.CreateQuery("SELECT [Id],[Name],[Price] FROM [Product] ORDER BY [Name] DESC");
			using var reader = command.ExecuteReader();

			if (!reader.HasRows)
			{
				return Array.Empty<Product>();
			}

			var products = new List<Product>();
			var idIndex = reader.GetOrdinal("Id");
			var customerIdIndex = reader.GetOrdinal("Name");
			var discountIndex = reader.GetOrdinal("Price");

			while (reader.Read())
			{
				products.Add(
					new Product(
						reader.GetInt32(idIndex),
						reader.GetString(customerIdIndex),
						reader.GetDecimal(discountIndex)
					)
				);
			}

			return products.ToArray();
		}

		public int Insert(CreateProductCommand dto)
		{
			using var connection = CreateConnection();
			using var command = connection.CreateProcedure("sp_AddProduct");

			command.Parameters.AddWithValue("@p_name", dto.Name);
			command.Parameters.AddWithValue("@p_price", dto.Price);
			var id = command.Parameters.AddOutput("@p_id", SqlDbType.Int);

			command.ExecuteNonQuery();

			return (int) id.Value;
		}

		public void Delete(int id)
		{
			using var connection = CreateConnection();
			using var command = connection.CreateProcedure("sp_DeleteProduct");

			command.Parameters.AddWithValue("@p_id", id);

			command.ExecuteNonQuery();
		}

		public void Update(int id, CreateProductCommand dto)
		{
			using var connection = CreateConnection();
			using var command = connection.CreateProcedure("sp_UpdateProduct");

			command.Parameters.AddWithValue("@p_id", id);
			command.Parameters.AddWithValue("@p_name", dto.Name);
			command.Parameters.AddWithValue("@p_price", dto.Price);

			command.ExecuteNonQuery();
		}
	}
}