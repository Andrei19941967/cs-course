using System;

namespace Reminder.Storage.WebApi.Dto
{
	public class CreateReminderItemDto
	{
		public string Message { get; set; }
		public string ContactId { get; set; }
		public DateTimeOffset DateTime { get; set; }
	}
}