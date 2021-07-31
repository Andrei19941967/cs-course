using System;

namespace L30_C02_working_with_sql_db_final.Orders
{
	public class CreateOrderCommand
	{
		public class Line
		{
			public int ProductId { get; set; }
			public int Count { get; set; }

			public Line(int productId, int count)
			{
				ProductId = productId;
				Count = count;
			}
		}

		public int CustomerId { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public decimal Discount { get; set; }
		public Line[] OrderLines { get; set; }

		public CreateOrderCommand(
			int customerId, 
			DateTimeOffset orderDate, 
			decimal discount, 
			params Line[] orderLines)
		{
			CustomerId = customerId;
			OrderDate = orderDate;
			Discount = discount;
			OrderLines = orderLines;
		}
	}
}