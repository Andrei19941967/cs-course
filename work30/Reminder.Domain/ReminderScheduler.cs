using System;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Reminder.Domain
{
	using Storage;
	using Sender;
	using Sender.Exceptions;
	using Receiver;

	public class ReminderScheduler : IDisposable
	{
		public event EventHandler<ReminderEventArgs> ReminderSent;
		public event EventHandler<ReminderEventArgs> ReminderFailed;

		private Timer _createdTimer;
		private Timer _readyTimer;
		private readonly ILogger _logger;
		private readonly IReminderStorage _storage;
		private readonly IReminderSender _sender;
		private readonly IReminderReceiver _receiver;

		public ReminderScheduler(
			ILogger<ReminderScheduler> logger,
			IReminderStorage storage,
			IReminderSender sender,
			IReminderReceiver receiver)
		{
			_logger = logger;
			_storage = storage;
			_sender = sender;
			_receiver = receiver;
		}

		public void Start(ReminderSchedulerSettings settings)
		{
			_logger.LogInformation("Starting messages receiving");
			_receiver.MessageReceived += OnMessageReceived;
			_receiver.Listen();

			_logger.LogInformation("Starting reminders scheduling");
			_createdTimer = new Timer(OnCreatedTimerTick, null, settings.TimerDelay, settings.CreatedTimerPeriod);
			_readyTimer = new Timer(OnReadyTimerTick, null, settings.TimerDelay, settings.ReadyTimerPeriod);
		}

		public void Dispose()
		{
			_logger.LogInformation("Stopping messages receiving");
			_receiver.MessageReceived -= OnMessageReceived;

			_logger.LogInformation("Stopping reminders scheduling");
			_createdTimer?.Dispose();
			_readyTimer?.Dispose();
		}

		private void OnCreatedTimerTick(object _)
		{
			_logger.LogDebug("Ticked timer for created items");

			var datetime = DateTimeOffset.UtcNow;
			var items = _storage.Find(datetime, ReminderItemStatus.Created);

			foreach (var item in items)
			{
				_logger.LogInformation($"Mark reminder {item.Id:N} as ready");
				item.MarkReady();
				_storage.Update(item);
			}
		}

		private void OnReadyTimerTick(object _)
		{
			_logger.LogDebug("Ticked timer for ready items");

			var items = _storage.Find(null, ReminderItemStatus.Ready);

			foreach (var item in items)
			{
				try
				{
					_logger.LogInformation($"Sending reminder {item.Id:N}");
					_sender.Send(new ReminderNotification(item.Message, item.ContactId));
					OnReminderSuccessful(item);
				}
				catch (ReminderNotificationException exception)
				{
					_logger.LogError(exception, "Exception occured while sending notification");
					OnReminderFailed(item);
				}
			}
		}

		private void OnMessageReceived(object sender, MessageReceivedEventArgs args)
		{
			_logger.LogDebug("Received message from receiver");

			var item = new ReminderItem(
				args.Message.Text,
				args.ContactId,
				args.Message.DateTime
			);
			_storage.Add(item);
			_logger.LogInformation($"Created reminder {item.Id:N}");
		}

		private void OnReminderSuccessful(ReminderItem item)
		{
			_logger.LogInformation($"Mark reminder {item.Id:N} as sent");
			item.MarkSuccessful();
			_storage.Update(item);
			ReminderSent?.Invoke(this, new ReminderEventArgs(item));
		}

		private void OnReminderFailed(ReminderItem item)
		{
			_logger.LogWarning($"Mark reminder {item.Id:N} as failed");
			item.MarkFailed();
			_storage.Update(item);
			ReminderFailed?.Invoke(this, new ReminderEventArgs(item));
		}
	}
}