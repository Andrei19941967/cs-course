using System;

namespace L30_C02_working_with_sql_db_final_async.Orders
{
	public class Order
	{
		public int Id { get; }
		public string Customer { get; }
		public DateTimeOffset OrderDate { get; }
		public decimal? Discount { get; }
		public decimal Total { get; }

		public Order(
			int id,
			string customer,
			DateTimeOffset orderDate,
			decimal? discount,
			decimal total)
		{
			Id = id;
			Customer = customer;
			OrderDate = orderDate;
			Discount = discount;
			Total = total;
		}

		public override string ToString() =>
			$"Order with id: {Id} for {Customer} at: {OrderDate} (Total: {Total}, Discount: {Discount.GetValueOrDefault()})";
	}
}