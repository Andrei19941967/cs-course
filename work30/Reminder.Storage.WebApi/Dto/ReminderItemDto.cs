using System;

namespace Reminder.Storage.WebApi.Dto
{
	public class ReminderItemDto
	{
		public Guid Id { get; set; }
		public string Message { get; set; }
		public string ContactId { get; set; }
		public DateTimeOffset DateTime { get; set; }
		public ReminderItemStatus Status { get; set; }
	}
}