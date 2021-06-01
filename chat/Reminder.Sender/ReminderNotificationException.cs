using System;

namespace Reminder.Sender
{
	public class ReminderNotificationException : Exception
	{
		public ReminderNotificationException(string message, Exception exception) : 
			base(message, exception)
		{
		}
	}
}