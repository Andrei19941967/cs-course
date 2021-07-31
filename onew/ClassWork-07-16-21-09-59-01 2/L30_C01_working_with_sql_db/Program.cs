using System;

namespace L30_C01_working_with_sql_db
{
	internal class Program
	{
		private const string ConnectionString =
			"Server=tcp:camelot-sql-server.database.windows.net,1433;" +
			"Initial Catalog=reminder; " +
			"Persist Security Info=False;" +
			"User ID=<username>@camelot-sql-server;" +
			"Password=<password>;" +
			"Encrypt=True;";

		private static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
		}
	}
}