using System;
using System.Collections.Generic;
using System.Linq;

namespace Reminder.Storage.Memory
{
	using Exceptions;

	public class ReminderStorage : IReminderStorage
	{
		private readonly Dictionary<Guid, ReminderItem> _items;

		public ReminderStorage()
		{
			_items = new Dictionary<Guid, ReminderItem>();
		}

		public void Add(ReminderItem item)
		{
			var result = _items.TryAdd(item.Id, item);
			if (!result)
			{
				throw new ReminderItemAlreadyExistsException(item.Id);
			}
		}

		public void Update(ReminderItem item)
		{
			if (!_items.TryGetValue(item.Id, out _))
			{
				throw new ReminderItemNotExistsException(item.Id);
			}

			_items[item.Id] = item;
		}

		public void Delete(Guid id)
		{
			if (!_items.Remove(id))
			{
				throw new ReminderItemNotExistsException(id);
			}
		}

		public ReminderItem Get(Guid id)
		{
			if (!_items.TryGetValue(id, out var item))
			{
				throw new ReminderItemNotExistsException(id);
			}

			return item;
		}

		public ReminderItem[] Find(DateTimeOffset? dateTime, ReminderItemStatus status)
		{
			var items = _items.Values.AsEnumerable();

			if (dateTime.HasValue)
			{
				items = items.Where(item => item.DateTime <= dateTime.Value);
			}

			return items
				.Where(item => item.Status == status)
				.OrderBy(item => item.DateTime)
				.ToArray();
		}
	}
}