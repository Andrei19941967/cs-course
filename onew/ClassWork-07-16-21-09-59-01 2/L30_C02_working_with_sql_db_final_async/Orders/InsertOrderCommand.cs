using System;
using System.Collections.Generic;

namespace L30_C02_working_with_sql_db_final_async.Orders
{
	public class InsertOrderCommand
	{
		public int CustomerId { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public double? Discount { get; set; }
		public List<(int productId, int count)> OrderLines { get; set; }

		public InsertOrderCommand(int customerId, double? discount = default)
		{
			CustomerId = customerId;
			OrderDate = DateTimeOffset.UtcNow;
			Discount = discount;
			OrderLines = new List<(int productId, int count)>();
		}
	}
}