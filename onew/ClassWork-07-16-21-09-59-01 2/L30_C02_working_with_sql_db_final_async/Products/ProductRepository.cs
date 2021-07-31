using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace L30_C02_working_with_sql_db_final_async.Products
{
	public class ProductRepository : BaseRepository, IProductRepository
	{
		public ProductRepository(string connection) :
			base(connection)
		{
		}

		public async Task<int> GetCountAsync()
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateQuery("SELECT COUNT(*) FROM [Product]");

			return (int) command.ExecuteScalar();
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateQuery("SELECT * FROM [Product] WHERE Id = @id");
			command.Parameters.AddWithValue("id", id);

			await using var reader = await command.ExecuteReaderAsync();
			var product = await ReadProductsAsync(reader).FirstOrDefaultAsync();
			if (product is null)
			{
				throw new ArgumentException($"Product with id {id} not found");
			}

			return product;
		}

		public async Task<Product[]> GetAllAsync()
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateQuery("SELECT * FROM [Product] ORDER BY [Name] DESC");
			await using var reader = await command.ExecuteReaderAsync();

			return await ReadProductsAsync(reader).ToArrayAsync();
		}

		public async Task<int> Insert(InsertProductCommand dto)
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateProcedure("InsertProduct");

			command.Parameters.AddWithValue("p_name", dto.Name);
			command.Parameters.AddWithValue("p_price", dto.Price);
			var productId = command.Parameters.AddOutput("p_id", SqlDbType.Int);

			await command.ExecuteNonQueryAsync();

			return (int) productId.Value;
		}

		private static async IAsyncEnumerable<Product> ReadProductsAsync(SqlDataReader reader)
		{
			if (!reader.HasRows)
			{
				yield break;
			}

			var id = reader.GetOrdinal("Id");
			var name = reader.GetOrdinal("Name");
			var price = reader.GetOrdinal("Price");

			while (await reader.ReadAsync())
			{
				var product = new Product(
					reader.GetInt32(id),
					reader.GetString(name),
					reader.GetDecimal(price)
				);
				yield return product;
			}
		}
	}
}