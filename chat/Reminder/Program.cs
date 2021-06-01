using System;

namespace Reminder
{
	using Domain;
	using Receiver;
	using Receiver.Telegram;
	using Sender;
	using Sender.Telegram;
	using Storage.Memory;

	public class FakeReminderSender : IReminderSender
	{
		public void Send(ReminderNotification notification) =>
			Console.WriteLine(notification.Message);
	}

	public class FakeReceiver : IReminderReceiver
	{
		public event EventHandler<MessageReceivedEventArgs> MessageReceived;

		public void Listen()
		{
			var items = new MessageReceivedEventArgs[]
			{
				new MessageReceivedEventArgs("Hello", "", DateTimeOffset.UtcNow.AddSeconds(10)),
				new MessageReceivedEventArgs("Hello 2", "", DateTimeOffset.UtcNow.AddSeconds(30)),
				new MessageReceivedEventArgs("Hello 3", "", DateTimeOffset.UtcNow.AddSeconds(60)),
			};

			foreach (var args in items)
			{
				MessageReceived?.Invoke(this, args);
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			const string token = "1851502724:AAECPTP6ydoWYuzBMBrzyTa6ITbAY0Z2D0o";

			using var scheduler = new ReminderScheduler(
				new ReminderStorage(),
				new ReminderSender(token),
				new ReminderReceiver(token)
			);

			scheduler.Start(
				new ReminderSchedulerSettings(
					TimeSpan.Zero, 
					TimeSpan.FromSeconds(1), 
					TimeSpan.FromSeconds(5))
			);

			Console.ReadLine();
		}
	}
}