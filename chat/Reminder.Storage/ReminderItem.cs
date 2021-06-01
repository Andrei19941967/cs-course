using System;

namespace Reminder.Storage
{
	/// <summary>
	///    Состояние одной напоминалки
	/// </summary>
	public class ReminderItem
	{
		/// <summary>
		///   Уникальный идентификатор напоминалки
		/// </summary>
		public Guid Id { get; private set; }

		/// <summary>
		///   Сообщение - напоминание
		/// </summary>
		public string Message { get; private set; }

		public string ContactId { get; private set; }
		public DateTimeOffset DateTime { get; private set; }
		public ReminderItemStatus Status { get; private set; }

		/// <summary>
		///    Создает новый объект.
		///    Генерирует уникальный идентификатор и статус Created
		/// </summary>
		/// <param name="message"></param>
		/// <param name="contactId"></param>
		/// <param name="dateTime"></param>
		public ReminderItem(string message, string contactId, DateTimeOffset dateTime)
		{
			Id = Guid.NewGuid();
			Message = message;
			DateTime = dateTime;
			ContactId = contactId;
			Status = ReminderItemStatus.Created;
		}

		public void MarkReady() =>
			ChangeStatus(ReminderItemStatus.Created, ReminderItemStatus.Ready);

		public void MarkSuccessful() =>
			ChangeStatus(ReminderItemStatus.Ready, ReminderItemStatus.Successful);

		public void MarkFailed() =>
			ChangeStatus(ReminderItemStatus.Ready, ReminderItemStatus.Failed);

		private void ChangeStatus(ReminderItemStatus from, ReminderItemStatus to)
		{
			if (Status != from)
			{
				throw new InvalidOperationException(
					$"Reminder status must be {from} to mark as {to}"
				);
			}

			Status = to;
		}
	}
}