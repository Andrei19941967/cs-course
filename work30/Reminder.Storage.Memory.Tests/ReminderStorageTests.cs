using System;
using NUnit.Framework;

namespace Reminder.Storage.Memory.Tests
{
	using Exceptions;
	using Reminder.Tests;

	public class ReminderStorageTests
	{
		[Test]
		public void Get_GivenNotExistingId_ShouldRaiseException()
		{
			var storage = new ReminderStorage();
			var itemId = Guid.NewGuid();

			var exception = Assert.Catch<ReminderItemNotExistsException>(() =>
				storage.Get(itemId)
			);
			Assert.AreEqual(itemId, exception.Id);
		}

		[Test]
		public void Get_GivenExistingItem_ShouldReturnIt()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = Create.Reminder.WithId(itemId).Please();
			var storage = new ReminderStorage(item);

			// Act
			var result = storage.Get(itemId);

			// Assert
			Assert.AreEqual(itemId, result.Id);
		}

		[Test]
		public void Add_GivenNotExistingId_ShouldGetByIdAfterAdd()
		{
			// Arrange
			var item = Create.Reminder.Please();
			var storage = new ReminderStorage();

			// Act
			storage.Add(item);
			var result = storage.Get(item.Id);

			// Assert
			Assert.AreEqual(item.Id, result.Id);
		}

		[Test]
		public void Add_GivenExistingItem_ShouldRaiseException()
		{
			// Arrange
			var item = Create.Reminder.Please();
			var storage = new ReminderStorage(item);

			// Act
			var exception = Assert.Catch<ReminderItemAlreadyExistsException>(() =>
				storage.Add(item)
			);

			// Assert
			Assert.AreEqual(item.Id, exception.Id);
		}

		[Test]
		public void Update_GivenNotExistingId_ShouldRaiseException()
		{
			var storage = new ReminderStorage();
			var item = Create.Reminder.Please();

			var exception = Assert.Catch<ReminderItemNotExistsException>(() =>
				storage.Update(item)
			);
			Assert.AreEqual(item.Id, exception.Id);
		}

		[Test]
		public void Update_GivenExistingItem_ShouldReturnUpdatedValues()
		{
			// Arrange
			var item = Create.Reminder
				.WithMessage("Initial message")
				.WithContact("Initial contact")
				.Please();
			var storage = new ReminderStorage(item);

			// Act
			var updatedItem = Create.Reminder
				.WithId(item.Id)
				.WithMessage("Updated message")
				.WithContact("Updated contact")
				.Please();
			storage.Update(updatedItem);
			var result = storage.Get(item.Id);

			// Assert
			Assert.AreEqual(updatedItem.Message, result.Message);
			Assert.AreEqual(updatedItem.ContactId, result.ContactId);
		}

		[Test]
		public void Find_GivenRemindersInFuture_ShouldReturnEmptyCollection()
		{
			var datetime = DateTimeOffset.UtcNow;
			var storage = new ReminderStorage(
				Create.Reminder.AtDatetime(datetime.AddMinutes(1)),
				Create.Reminder.AtDatetime(datetime.AddSeconds(1))
			);

			var result = storage.Find(datetime, ReminderItemStatus.Created);

			CollectionAssert.IsEmpty(result);
		}

		[Test]
		public void Find_GivenRemindersInPastOrEqual_ShouldReturnNotEmptyCollection()
		{
			var datetime = DateTimeOffset.UtcNow;
			var storage = new ReminderStorage(
				Create.Reminder.AtDatetime(datetime.AddMinutes(-1)),
				Create.Reminder.AtDatetime(datetime)
			);

			var result = storage.Find(datetime, ReminderItemStatus.Created);

			CollectionAssert.IsNotEmpty(result);
		}

		[Test]
		public void Find_GivenRemindersWithStatus_ShouldReturnNotEmptyCollection()
		{
			var status = ReminderItemStatus.Created;
			var storage = new ReminderStorage(
				Create.Reminder.WithStatus(status)
			);

			var result = storage.Find(default, status);

			CollectionAssert.IsNotEmpty(result);
		}
	}
}