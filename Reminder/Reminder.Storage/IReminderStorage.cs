using System;

namespace Reminder.Storage
{
    public interface IReminderStorage
    {
        void Add(ReminderItem item);
        void Update(ReminderItem item);
        void Delete(Guid id);
        ReminderItem Get(Guid id);
    }
}