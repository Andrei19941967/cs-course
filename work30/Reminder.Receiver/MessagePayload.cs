using System;
using System.Collections.Generic;

namespace Reminder.Receiver
{
	public class MessagePayload
	{
		public static readonly string[] Separators = {"\n", "\t", ",", ";"};

		public static IEnumerable<string> WellKnownKeys =>
			OffsetMap.Keys;

		private static readonly Dictionary<string, Func<double, TimeSpan>> OffsetMap =
			new Dictionary<string, Func<double, TimeSpan>>
			{
				["sec"] = TimeSpan.FromSeconds,
				["min"] = TimeSpan.FromMinutes,
				["hour"] = TimeSpan.FromHours,
				["day"] = TimeSpan.FromDays,
			};

		public DateTimeOffset DateTime { get; }
		public string Text { get; }

		public MessagePayload(string text, DateTimeOffset dateTime)
		{
			Text = text;
			DateTime = dateTime;
		}

		public static bool TryParse(string message, out MessagePayload payload)
		{
			payload = default;

			if (string.IsNullOrWhiteSpace(message))
			{
				return false;
			}

			var parts = message.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length == 0)
			{
				return false;
			}

			var text = parts[0].Trim();
			payload = parts.Length switch
			{
				1 => new MessagePayload(text, DateTimeOffset.UtcNow),
				> 1 when TryParseDateTime(parts[1], out var datetime) => new MessagePayload(text, datetime),
				_ => payload
			};

			return payload != default;
		}

		private static bool TryParseDateTime(string text, out DateTimeOffset datetime)
		{
			var parts = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length != 2)
			{
				return DateTimeOffset.TryParse(text, out datetime);
			}

			if (!int.TryParse(parts[0], out var count))
			{
				return DateTimeOffset.TryParse(text, out datetime);
			}

			if (!OffsetMap.TryGetValue(parts[1].ToLower(), out var offset))
			{
				return DateTimeOffset.TryParse(text, out datetime);
			}

			datetime = DateTimeOffset.UtcNow.Add(offset(count));
			return true;
		}
	}
}