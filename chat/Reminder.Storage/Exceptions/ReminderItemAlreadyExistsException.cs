using System;

namespace Reminder.Storage.Exceptions
{
	public class ReminderItemAlreadyExistsException : Exception
	{
		public ReminderItemAlreadyExistsException(Guid id) :
			base($"Reminder item with id {id} already exists")
		{
		}
	}
}