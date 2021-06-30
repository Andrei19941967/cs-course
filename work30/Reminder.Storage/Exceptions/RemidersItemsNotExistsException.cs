using System;

namespace Reminder.Storage.Exceptions
{
    public class RemidersItemsNotExistsException: Exception
    {
        public RemidersItemsNotExistsException() :
            base($"Reminders item  not exists")
        {
        }
    }
}