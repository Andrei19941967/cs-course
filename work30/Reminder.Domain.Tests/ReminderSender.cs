using Reminder.Sender;
using Reminder.Sender.Exceptions;

namespace Reminder.Domain.Tests
{
	public class ReminderSender : IReminderSender
	{
		private readonly bool _fail;

		public ReminderSender(bool fail)
		{
			_fail = fail;
		}

		public void Send(ReminderNotification item)
		{
			if (_fail)
			{
				throw new ReminderNotificationException("bad");
			}
		}
	}
}