using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Reminder.Receiver.Telegram
{
	public class ReminderReceiver : IReminderReceiver
	{
		private readonly ITelegramBotClient _client;

		public event EventHandler<MessageReceivedEventArgs> MessageReceived;

		public ReminderReceiver(string token)
		{
			_client = new TelegramBotClient(token);
			_client.OnMessage += OnMessage;
		}

		public void Listen()
		{
			_client.StartReceiving();
		}

		private void OnMessage(object sender, MessageEventArgs args)
		{
			var message = $@"Invalid message format.
Message should be formatted as following: <Message><Separators><DateTimeUtc>
Where:
- <Message> is regular unicode text
- <Separators> can be any of following values: {string.Join(',', MessagePayload.Separators.Select(_ => $"\"{_}\""))}
- <DateTimeUtc> can be absolute date time in utc time format; 
                    or relative value in following format: <Value> <Key>, where <Value> is a number, and <Key> is one of following values: {string.Join(',', MessagePayload.WellKnownKeys)};
                    messages without datetime returns immediately";

			if (!MessagePayload.TryParse(args.Message.Text, out var payload))
			{
				_client.SendTextMessageAsync(args.Message.Chat.Id, message);
				return;
			}

			var contactId = args.Message.Chat.Id;
			var eventArgs = new MessageReceivedEventArgs(payload, contactId.ToString());

			MessageReceived?.Invoke(this, eventArgs);
		}
	}
}