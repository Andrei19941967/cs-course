using System;

namespace Reminder.Receiver
{
	public interface IReminderReceiver
	{
		event EventHandler<MessageReceivedEventArgs> MessageReceived;

		void Listen();
	}
}