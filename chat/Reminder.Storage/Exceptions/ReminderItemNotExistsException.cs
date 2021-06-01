using System;

namespace Reminder.Storage.Exceptions
{
	public class ReminderItemNotExistsException : Exception
	{
		public ReminderItemNotExistsException(Guid id) :
			base($"Reminder item with id {id} not exists")
		{
		}
	}
}