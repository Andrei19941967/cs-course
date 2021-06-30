using System;
using NUnit.Framework;

namespace Reminder.Receiver.Tests
{
	public class MessagePayloadTests
	{
		[TestCase(null)]
		[TestCase("")]
		[TestCase("               ")]
		public void GivenNullOrEmptyString_WhenTryParseInvoked_ThenReturnsNull(string text)
		{
			var result = MessagePayload.TryParse(text, out var payload);

			Assert.IsFalse(result);
			Assert.IsNull(payload);
		}

		[TestCase("aa")]
		public void GivenStringWithoutDatetime_WhenTryParseInvoked_ThenReturnsNowDatetime(string text)
		{
			var result = MessagePayload.TryParse(text, out var payload);

			Assert.IsTrue(result);
			Assert.IsNotNull(payload);
		}

		[TestCase("aa\na")]
		public void GivenStringWithoutCorrectDatetime_WhenTryParseInvoked_ThenReturnsNull(string text)
		{
			var result = MessagePayload.TryParse(text, out var payload);

			Assert.IsFalse(result);
			Assert.IsNull(payload);
		}

		[TestCase("2020.05.25T20:00:08", "Message", '\n')]
		[TestCase("2020.05.25T20:00:08+03", "Message", '\n')]
		[TestCase("2020.05.25T20:00:08+03", "Message", ';')]
		[TestCase("2020.05.25T20:00:08+03", "Message", ',')]
		public void GivenStringInValidFormat_WhenParseInvoked_ThenReturnMessageWithComponents(
			string datetime,
			string text,
			char separator)
		{
			var result = MessagePayload.TryParse($"{text}{separator}{datetime}", out var message);

			Assert.IsTrue(result);
			Assert.AreEqual(DateTimeOffset.Parse(datetime), message.DateTime);
			Assert.AreEqual(text, message.Text);
		}

		[TestCase("10 sec", "Message", "\n")]
		public void GivenDatetimeStringInWellKnownFormats_WhenParseInvoked_ThenReturnDatetimeWithValidOffset(
			string datetime,
			string text,
			string separator)
		{
			var expectedDatetime = DateTimeOffset.UtcNow;
			var result = MessagePayload.TryParse($"{text}{separator}{datetime}", out var message);

			Assert.IsTrue(result);
			Assert.GreaterOrEqual(message.DateTime - expectedDatetime, TimeSpan.FromSeconds(10));
		}
	}
}