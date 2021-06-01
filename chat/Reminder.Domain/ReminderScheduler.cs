using System;
using System.Threading;
using ILogger;

namespace Reminder.Domain
{
	// todo: Implement tests on Scheduler (use fake implementations for IReminderSender, IReminderReceiver)
	// todo: Add more Logging with Logger (previous home work)
	// todo: Custom message formats & error handling 

	using Storage;
	using Sender;
	using Receiver;

	public class ReminderScheduler : IDisposable
	{
		private ConsoleLogger _logger;
		private Timer _createdTimer;
		private Timer _readyTimer;
		private readonly IReminderStorage _storage;
		private readonly IReminderSender _sender;
		private readonly IReminderReceiver _receiver;

		public ReminderScheduler(
			IReminderStorage storage,
			IReminderSender sender,
			IReminderReceiver receiver)
		{
			_logger = ConsoleLogger.GetInstance();
			_storage = storage;
			_sender = sender;
			_receiver = receiver;
		}

		public void Start(ReminderSchedulerSettings settings)
		{
			_receiver.MessageReceived += OnMessageReceived;
			_receiver.Listen();
			_createdTimer = new Timer(OnCreatedTimerTick, null, settings.TimerDelay, settings.CreatedTimerPeriod);
			_readyTimer = new Timer(OnReadyTimerTick, null, settings.TimerDelay, settings.ReadyTimerPeriod);
		}

		public void Dispose()
		{
			_receiver.MessageReceived -= OnMessageReceived;
			_createdTimer?.Dispose();
			_readyTimer?.Dispose();
		}

		private void OnCreatedTimerTick(object _)
		{
			_logger.Info("On CreatedTimer tick");

			var datetime = DateTimeOffset.UtcNow;
			var items = _storage.Find(datetime, ReminderItemStatus.Created);

			foreach (var item in items)
			{
				item.MarkReady();
				_storage.Update(item);
				_logger.Info($"Item {item.Id} ready");
			}
		}

		private void OnReadyTimerTick(object _)
		{
			_logger.Info("On ReadyTimer tick");

			var items = _storage.Find(null, ReminderItemStatus.Ready);

			foreach (var item in items)
			{
				try
				{
					_sender.Send(new ReminderNotification(item.Message, item.ContactId));
					item.MarkSuccessful();
					_logger.Info($"Item {item.Id} sent.");
				}
				catch (ReminderNotificationException)
				{
					item.MarkFailed();
					_logger.Info($"Item {item.Id} sent failed!");
				}

				_storage.Update(item);
			}
		}

		private void OnMessageReceived(object sender, MessageReceivedEventArgs args)
		{
			Console.WriteLine("On MessageReceived tick");

			_storage.Add(
				new ReminderItem(
					args.Message,
					args.ContactId,
					args.DateTime
				)
			);
			
			_logger.Info($"Added to storage item:\n" +
			             $"Message: {args.Message}\n" +
			             $"ContactID: {args.ContactId}\n" +
			             $"Date: {args.DateTime.ToString()}");
		}
	}
}