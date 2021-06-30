using Reminder.Sender.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace Reminder.Sender.Telegram
{
	public class ReminderSender : IReminderSender
	{
		private readonly ITelegramBotClient _client;

		public ReminderSender(string token)
		{
			_client = new TelegramBotClient(token);
		}

		public void Send(ReminderNotification notification)
		{
			var chatId = new ChatId(
				long.Parse(notification.ContactId)
			);

			try
			{
				_client.SendTextMessageAsync(chatId, notification.Message)
					.GetAwaiter()
					.GetResult();
			}
			catch (ApiRequestException exception)
			{
				throw new ReminderNotificationException("Notification send failed", exception);
			}
		}
	}
}