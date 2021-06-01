using System;

namespace Reminder.Domain
{
	public class ReminderSchedulerSettings
	{
		public TimeSpan TimerDelay { get; }
		public TimeSpan CreatedTimerPeriod { get; }
		public TimeSpan ReadyTimerPeriod { get; }

		public ReminderSchedulerSettings(TimeSpan timerDelay, TimeSpan createdTimerPeriod, TimeSpan readyTimerPeriod)
		{
			TimerDelay = timerDelay;
			CreatedTimerPeriod = createdTimerPeriod;
			ReadyTimerPeriod = readyTimerPeriod;
		}
	}
}