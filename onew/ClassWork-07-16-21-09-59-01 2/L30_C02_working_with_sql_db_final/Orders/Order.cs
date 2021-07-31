using System;

namespace L30_C02_working_with_sql_db_final.Orders
{
	public class Order
	{
		public int Id { get; set; }
		public string Customer { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public decimal Discount { get; set; }
		public decimal Total { get; set; }

		public Order(
			int id, 
			string customer, 
			DateTimeOffset orderDate, 
			decimal discount, 
			decimal total)
		{
			Id = id;
			Customer = customer;
			OrderDate = orderDate;
			Discount = discount;
			Total = total;
		}
	}
}