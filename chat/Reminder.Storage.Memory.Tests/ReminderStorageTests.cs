using System;
using NUnit.Framework;
using Reminder.Storage.Exceptions;

namespace Reminder.Storage.Memory.Tests
{
	public class ReminderStorageTests
	{
		[Test]
		public void GivenReminder_WhenAdd_ReturnById()
		{
			var storage = new ReminderStorage();
			var item = new ReminderItem("Some", "Contact", DateTimeOffset.UtcNow);

			storage.Add(item);

			var found = storage.Get(item.Id);
			Assert.AreEqual(found.Message, item.Message);
		}

		[Test]
		public void GivenReminderWithSameId_WhenAdd_ThrowsException()
		{
			var storage = new ReminderStorage();
			var item = new ReminderItem("Some", "Contact", DateTimeOffset.UtcNow);

			storage.Add(item);

			Assert.Catch<ReminderItemAlreadyExistsException>(() => storage.Add(item));
		}
	}
}