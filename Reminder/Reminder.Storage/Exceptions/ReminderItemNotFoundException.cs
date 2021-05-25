using System;
using System.Runtime.InteropServices;

namespace Reminder.Storage.Exceptions
{
    public class ReminderItemNotFoundException: Exception
    {
        public ReminderItemNotFoundException(Guid id) :
            base($"Remider item with id {id} not found")
        {
            
        }
    }
}