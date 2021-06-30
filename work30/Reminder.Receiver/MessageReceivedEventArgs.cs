using System;

namespace Reminder.Receiver
{
	public class MessageReceivedEventArgs : EventArgs
	{
		public MessagePayload Message { get; }
		public string ContactId { get; }

		public MessageReceivedEventArgs(MessagePayload message, string contactId)
		{
			Message = message;
			ContactId = contactId;
		}
	}
}