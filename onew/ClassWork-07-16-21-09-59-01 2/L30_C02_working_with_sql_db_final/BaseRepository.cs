using Microsoft.Data.SqlClient;

namespace L30_C02_working_with_sql_db_final
{
	public abstract class BaseRepository
	{
		protected string ConnectionString { get; }

		protected BaseRepository(string connectionString)
		{
			ConnectionString = connectionString;
		}

		protected SqlConnection CreateConnection()
		{
			var connection = new SqlConnection(ConnectionString);
			connection.Open();

			return connection;
		}
	}
}