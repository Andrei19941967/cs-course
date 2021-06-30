using Reminder.Storage;

namespace Reminder.Domain
{
	public class ReminderEventArgs
	{
		public ReminderItem Reminder { get; }

		public ReminderEventArgs(ReminderItem reminder)
		{
			Reminder = reminder;
		}
	}
}