using System;
using NUnit.Framework;
using Reminder.Storage.Exceptions;

namespace Reminder.Storage.WebApi.Tests
{
	public class ReminderStorageTests
	{
		[Test]
		public void Get_WhenItemNotExists_ShouldRaiseNotFoundException()
		{
			var storage = new ReminderWebApplicationFactory()
				.CreateWebApiClient();

			Assert.Catch<ReminderItemNotExistsException>(
				() => storage.Get(Guid.NewGuid())
			);
		}

		[Test]
		public void Get_WhenItemExists_ShouldReturnIt()
		{
			var existingItem = new ReminderItem("Message", "Contact", DateTimeOffset.UtcNow);
			var storage = new ReminderWebApplicationFactory()
				.WithExistingItems(existingItem)
				.CreateWebApiClient();

			var item = storage.Get(existingItem.Id);

			Assert.AreEqual(existingItem.Id, item.Id);
			Assert.AreEqual(existingItem.Message, item.Message);
			Assert.AreEqual(existingItem.ContactId, item.ContactId);
			Assert.AreEqual(existingItem.DateTime, item.DateTime);
		}

		[Test]
		public void Add_WhenNotExists_ShouldReturnCreatedItem()
		{
			var item = new ReminderItem("Message", "Contact", DateTimeOffset.UtcNow);
			var storage = new ReminderWebApplicationFactory()
				.CreateWebApiClient();

			storage.Add(item);

			var receivedItem = storage.Get(item.Id);
			Assert.AreEqual(receivedItem.Id, item.Id);
			Assert.AreEqual(receivedItem.Message, item.Message);
			Assert.AreEqual(receivedItem.ContactId, item.ContactId);
			Assert.AreEqual(receivedItem.DateTime, item.DateTime);
		}
	}
}