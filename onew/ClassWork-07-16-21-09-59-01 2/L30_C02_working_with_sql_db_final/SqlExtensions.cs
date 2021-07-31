using System.Data;
using Microsoft.Data.SqlClient;

namespace L30_C02_working_with_sql_db_final
{
	public static class SqlExtensions
	{
		public static SqlCommand CreateQuery(this SqlConnection connection, 
			string text,
			SqlTransaction transaction = default)
		{
			var command = connection.CreateCommand();
			command.Transaction = transaction;
			command.CommandType = CommandType.Text;
			command.CommandText = text;
			return command;
		}

		public static SqlCommand CreateProcedure(this SqlConnection connection,
			string name,
			SqlTransaction transaction = default)
		{
			var command = connection.CreateCommand();
			command.Transaction = transaction;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = name;
			return command;
		}

		public static SqlParameter AddOutput(this SqlParameterCollection parameters,
			string name,
			SqlDbType type)
		{
			var parameter = new SqlParameter(name, type)
			{
				Direction = ParameterDirection.Output
			};
			parameters.Add(parameter);
			return parameter;
		}
	}
}