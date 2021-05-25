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


            public ReminderItem(string message, string contactId, DateTimeOffset dateTime)
            {
                Id = Guid.NewGuid();
                Message = message;
                DateTime = dateTime;
                ContactId = contactId;
                Status = ReminderItemStatus.Created;
            }
    }
}