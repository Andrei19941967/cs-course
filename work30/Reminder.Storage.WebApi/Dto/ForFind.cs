using System;

namespace Reminder.Storage.WebApi.Dto
{
    public class ForFind
    {
        public DateTimeOffset? DateTime { get; set; }
        public ReminderItemStatus Status { get; set; }
    }
}