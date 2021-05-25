using System;
using System.Collections.Generic;
using Reminder.Storage.Exceptions;

namespace Reminder.Storage.Memory
{
    public class ReminderStorage: IReminderStorage
    {
        private readonly Dictionary<Guid, ReminderItem> _items;

        public ReminderStorage()
        {
            _items = new Dictionary<Guid, ReminderItem>();
        }
        
        public void Add(ReminderItem item)
        {
            bool result = _items.TryAdd(item.Id, item);
            if (!result)
            {
                throw new ReminderItemAlreadyExistsException(item.Id);
            }
        }

        public void Update(ReminderItem item)
        {
            if (_items.ContainsKey(item.Id))
            {
                _items[item.Id] = item;
            }
            else
            {
                throw new ReminderItemNotFoundException(item.Id);
            }
        }

        public void Delete(Guid id)
        {
            if (_items.ContainsKey(id))
            {
                _items.Remove(id);
            }
            else
            {
                throw new ReminderItemNotFoundException(id);
            }
        }

        public ReminderItem Get(Guid id)
        {
            if (_items.ContainsKey(id))
            {
                return _items[id];
            }
            else
            {
                throw new ReminderItemNotFoundException(id);
            }
        }
    }
}