namespace Reminder.Tests
{
	public class Create
	{
		public static ReminderStorageBuilder Storage =>
			new ReminderStorageBuilder();

		public static ReminderItemBuilder Reminder =>
			new ReminderItemBuilder();
	}
}