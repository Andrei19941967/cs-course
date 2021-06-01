using System;
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
			var text = args.Message.Text;
			if (string.IsNullOrWhiteSpace(text))
			{
				return;
			}

			//если разделяем через запятую, то после нее берем секунды, которые добавляем к текущему времени
			var lines = text.Split(new char[]{'\n', ','});
			if (lines.Length < 2)
			{
				return;
			}

			var message = lines[0].Trim();
			if (!DateTimeOffset.TryParse(lines[1], out var datetime))
			{
				datetime = DateTimeOffset.UtcNow;
			}
			else
			{
				datetime = DateTimeOffset.UtcNow.AddSeconds(Convert.ToInt32(lines[1]));
			}

			MessageReceived?.Invoke(this,
				new MessageReceivedEventArgs(
					message,
					args.Message.Chat.Id.ToString(),
					datetime
				)
			);
		}
	}
}