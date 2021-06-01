namespace Reminder.Sender
{
	public interface IReminderSender
	{
		void Send(ReminderNotification notification);
	}
}