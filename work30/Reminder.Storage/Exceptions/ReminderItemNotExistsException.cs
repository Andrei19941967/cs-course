using System;

namespace Reminder.Storage.Exceptions
{
	public class ReminderItemNotExistsException : Exception
	{
		public Guid Id { get; }

		public ReminderItemNotExistsException(Guid id) :
			base($"Reminder item with id {id} not exists")
		{
			Id = id;
		}
	}
}