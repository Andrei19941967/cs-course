using System;

namespace Reminder.Storage
{
	public class ReminderItem
	{
		public Guid Id { get; private set; }
		public string Message { get; private set; }
		public string ContactId { get; private set; }
		public DateTimeOffset DateTime { get; private set; }
		public ReminderItemStatus Status { get; private set; }

		public ReminderItem(Guid id, string message, string contactId, DateTimeOffset dateTime, ReminderItemStatus status)
		{
			Id = id;
			Message = message;
			ContactId = contactId;
			DateTime = dateTime;
			Status = status;
		}

		public ReminderItem(string message, string contactId, DateTimeOffset dateTime)
		{
			Id = Guid.NewGuid();
			Message = message;
			DateTime = dateTime;
			ContactId = contactId;
			Status = ReminderItemStatus.Created;
		}

		public void MarkReady() =>
			ChangeStatus(ReminderItemStatus.Created, ReminderItemStatus.Ready);

		public void MarkSuccessful() =>
			ChangeStatus(ReminderItemStatus.Ready, ReminderItemStatus.Successful);

		public void MarkFailed() =>
			ChangeStatus(ReminderItemStatus.Ready, ReminderItemStatus.Failed);

		private void ChangeStatus(ReminderItemStatus from, ReminderItemStatus to)
		{
			if (Status != from)
			{
				throw new InvalidOperationException(
					$"Reminder status must be {from} to mark as {to}"
				);
			}

			Status = to;
		}
	}
}