using System;

namespace Reminder.Receiver
{
	public class MessageReceivedEventArgs
	{
		public string Message { get; }
		public string ContactId { get; }
		public DateTimeOffset DateTime { get; }

		public MessageReceivedEventArgs(string message, string contactId, DateTimeOffset dateTime)
		{
			Message = message;
			ContactId = contactId;
			DateTime = dateTime;
		}
	}
}