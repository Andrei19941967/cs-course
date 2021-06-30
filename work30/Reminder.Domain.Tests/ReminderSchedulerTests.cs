using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace Reminder.Domain.Tests
{
	using Sender;
	using Storage;
	using Reminder.Tests;

	public class ReminderSchedulerTests
	{
		public ReminderSchedulerSettings DefaultSettings =>
			new ReminderSchedulerSettings(
				TimeSpan.Zero,
				TimeSpan.FromMilliseconds(20),
				TimeSpan.FromMilliseconds(40)
			);

		public IReminderSender SuccessSender =>
			new ReminderSender(fail: false);

		public IReminderSender FailedSender =>
			new ReminderSender(fail: true);

		public ReminderReceiver Receiver { get; } =
			new ReminderReceiver();

		public IReminderStorage Storage =>
			Create.Storage.Build();

		public ILogger<ReminderScheduler> Logger =>
			NullLogger<ReminderScheduler>.Instance;

		[Test]
		public void GivenReminderWithPastDateAndSuccessSender_ShouldRaiseSentEvent()
		{
			var raised = false;
			var scheduler = new ReminderScheduler(Logger, Storage, SuccessSender, Receiver);
			scheduler.ReminderSent += (_, _) => raised = true;

			scheduler.Start(DefaultSettings);
			Receiver.SendMessage(DateTimeOffset.UtcNow, "Message", "ContactId");
			WaitTimers();

			Assert.IsTrue(raised);
		}

		[Test]
		public void GivenReminderWithPastDateAndFailedSender_ShouldRaiseFailedEvent()
		{
			var raised = false;
			var scheduler = new ReminderScheduler(Logger, Storage, FailedSender, Receiver);
			scheduler.ReminderFailed += (_, _) => raised = true;

			scheduler.Start(DefaultSettings);
			Receiver.SendMessage(DateTimeOffset.UtcNow, "Message", "ContactId");
			WaitTimers();

			Assert.IsTrue(raised);
		}

		private void WaitTimers()
		{
			var interval = DefaultSettings.CreatedTimerPeriod +
						   DefaultSettings.ReadyTimerPeriod +
						   DefaultSettings.TimerDelay;

			Thread.Sleep(interval.Milliseconds * 2);
		}
	}
}