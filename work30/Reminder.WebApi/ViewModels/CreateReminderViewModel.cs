using System;
using System.ComponentModel.DataAnnotations;

namespace Reminder.WebApi.ViewModels
{
	public class CreateReminderViewModel
	{
		[Required]
		[StringLength(250)]
		public string Message { get; set; }

		[Required]
		[StringLength(250)]
		public string ContactId { get; set; }

		public DateTimeOffset DateTime { get; set; }
	}
}