using System;
using Reminder.Receiver;

namespace Reminder.Domain.Tests
{
	public class ReminderReceiver : IReminderReceiver
	{
		public event EventHandler<MessageReceivedEventArgs> MessageReceived;

		public void Listen()
		{
		}

		public void SendMessage(DateTimeOffset datetime, string message, string contactId) =>
			MessageReceived?.Invoke(this,
				new MessageReceivedEventArgs(
					new MessagePayload(message, datetime), contactId));
	}
}