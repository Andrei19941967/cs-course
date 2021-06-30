using System;
using Microsoft.Extensions.Logging;

namespace Reminder
{
	using Domain;
	using Receiver.Telegram;
	using Sender.Telegram;
	using Storage.Memory;

	internal class Program
	{
		private const string TelegramToken = "PASTE YOUR TOKEN HERE";

		private static readonly ILoggerFactory Logging = LoggerFactory.Create(_ =>
			{
				_.AddConsole();
				_.SetMinimumLevel(LogLevel.Trace);
			}
		);

		private static readonly ILogger Logger = Logging.CreateLogger<Program>();

		static void Main(string[] args)
		{
			using var scheduler = new ReminderScheduler(
				Logging.CreateLogger<ReminderScheduler>(),
				new ReminderStorage(),
				new ReminderSender(TelegramToken),
				new ReminderReceiver(TelegramToken)
			);

			scheduler.ReminderSent += OnReminderSent;
			scheduler.ReminderFailed += OnReminderFailed;
			scheduler.Start(
				new ReminderSchedulerSettings(
					TimeSpan.Zero,
					TimeSpan.FromSeconds(1),
					TimeSpan.FromSeconds(5))
			);

			Logger.LogInformation("Waiting reminders..");
			Logger.LogInformation("Press any key to stop");
			Console.ReadKey();
		}

		private static void OnReminderSent(object sender, ReminderEventArgs args) =>
			Logger.LogInformation(
				$"Reminder ({args.Reminder.Id}) at " +
				$"{args.Reminder.DateTime:F} sent received with " +
				$"message {args.Reminder.Message}"
			);

		private static void OnReminderFailed(object sender, ReminderEventArgs args) =>
			Logger.LogWarning(
				$"Reminder ({args.Reminder.Id}) at " +
				$"{args.Reminder.DateTime:F} sent failed with " +
				$"message {args.Reminder.Message}"
			);
	}
}