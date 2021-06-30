using System.ComponentModel.DataAnnotations;
using Reminder.Storage;

namespace Reminder.WebApi.ViewModels
{
	public class UpdateReminderViewModel
	{
		[Required]
		[StringLength(250)]
		public string Message { get; set; }

		public ReminderItemStatus Status { get; set; }
	}
}