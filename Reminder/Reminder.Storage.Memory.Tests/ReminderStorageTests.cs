using System;
using NUnit.Framework;
using Reminder.Storage.Exceptions;

namespace Reminder.Storage.Memory.Tests
{
    public class ReminderStorageTests
    {
        [Test]
        public void Test_Add1()
        {
            ReminderStorage storage = new ReminderStorage();
            ReminderItem item = new ReminderItem("Hello", "Contact", DateTimeOffset.UtcNow);
            
            storage.Add(item);

            ReminderItem found = storage.Get(item.Id);

            Assert.AreEqual(item.Message, found.Message);

        }

        [Test]
        public void Test_Add2()
        {
            ReminderStorage storage = new ReminderStorage();
            ReminderItem item = new ReminderItem("Hello", "Contact", DateTimeOffset.UtcNow);
            
            storage.Add(item);

            Assert.Catch<ReminderItemAlreadyExistsException>(() => storage.Add(item));
        }

        [Test]
        public void Test_Get1()
        {
            ReminderStorage storage = new ReminderStorage();
            ReminderItem item = new ReminderItem("Hello", "Contact", DateTimeOffset.UtcNow);
            ReminderItem itemNotAdded = new ReminderItem("Bye", "Contact", DateTimeOffset.UtcNow);
            
            storage.Add(item);
            
            Assert.Catch<ReminderItemNotFoundException>(() => storage.Get(itemNotAdded.Id));
        }
        
        [Test]
        public void Test_Get2()
        {
            ReminderStorage storage = new ReminderStorage();
            ReminderItem item = new ReminderItem("Hello", "Contact", DateTimeOffset.UtcNow);

            storage.Add(item);
            
           Assert.AreEqual(item, storage.Get(item.Id));
        }

        [Test]
        public void Test_Delete1()
        {
            ReminderStorage storage = new ReminderStorage();
            ReminderItem item = new ReminderItem("Hello", "Contact", DateTimeOffset.UtcNow);

            storage.Add(item);
            storage.Delete(item.Id);
            
            Assert.Catch<ReminderItemNotFoundException>(() => storage.Get(item.Id));
        }

        [Test]
        public void Test_Delete2()
        {
            ReminderStorage storage = new ReminderStorage();
            ReminderItem item = new ReminderItem("Hello", "Contact", DateTimeOffset.UtcNow);
            ReminderItem itemNotAdded = new ReminderItem("Bye", "Contact", DateTimeOffset.UtcNow);
            
            storage.Add(item);
            
            Assert.Catch<ReminderItemNotFoundException>(() => storage.Delete(itemNotAdded.Id));
        }

        [Test]
        public void Test_Update1()
        {
            ReminderStorage storage = new ReminderStorage();
            ReminderItem item = new ReminderItem("Hello", "Contact", DateTimeOffset.UtcNow);
            
            Assert.Catch<ReminderItemNotFoundException>(() => storage.Update(item));
        }
    }
}