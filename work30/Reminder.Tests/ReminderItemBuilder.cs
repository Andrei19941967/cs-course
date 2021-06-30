using System;
using Reminder.Storage;

namespace Reminder.Tests
{
	public class ReminderItemBuilder
	{
		private Guid _id = Guid.NewGuid();
		private ReminderItemStatus _status = ReminderItemStatus.Created;
		private DateTimeOffset _datetime = DateTimeOffset.UtcNow;
		private string _message = "Test";
		private string _contact = "Contact";

		public ReminderItemBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public ReminderItemBuilder WithMessage(string message)
		{
			_message = message;
			return this;
		}

		public ReminderItemBuilder WithContact(string contact)
		{
			_contact = contact;
			return this;
		}

		public ReminderItemBuilder WithStatus(ReminderItemStatus status)
		{
			_status = status;
			return this;
		}

		public ReminderItemBuilder AtDatetime(DateTimeOffset datetime)
		{
			_datetime = datetime;
			return this;
		}

		public ReminderItemBuilder AtUtcNow() =>
			AtDatetime(DateTimeOffset.UtcNow);

		public ReminderItem Please() =>
			new ReminderItem(_id, _message, _contact, _datetime, _status);

		public static implicit operator ReminderItem(ReminderItemBuilder builder) =>
			builder.Please();
	}
}