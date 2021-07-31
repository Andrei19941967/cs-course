using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace L30_C02_working_with_sql_db_final_async
{
	public class BaseRepository
	{
		protected string ConnectionString { get; }

		public BaseRepository(string connectionString)
		{
			ConnectionString = connectionString;
		}

		protected async Task<SqlConnection> GetConnection()
		{
			var connection = new SqlConnection(ConnectionString);
			await connection.OpenAsync();
			return connection;
		}
	}
}