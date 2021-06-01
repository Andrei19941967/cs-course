namespace Reminder.Sender
{
	public class ReminderNotification
	{
		public string Message { get; }
		public string ContactId { get; }

		public ReminderNotification(string message, string contactId)
		{
			Message = message;
			ContactId = contactId;
		}
	}
}