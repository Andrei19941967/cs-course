using System;

namespace Reminder.Sender.Exceptions
{
	public class ReminderNotificationException : Exception
	{
		public ReminderNotificationException(string message) : base(message)
		{
		}

		public ReminderNotificationException(string message, Exception exception) :
			base(message, exception)
		{
		}
	}
}