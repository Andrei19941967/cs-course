using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Reminder.Storage.WebApi
{
	using Exceptions;
	using Dto;

	public class ReminderStorage : IReminderStorage
	{
		private readonly HttpClient _client;

		public ReminderStorage(string url)
		{
			_client = new HttpClient
			{
				BaseAddress = new Uri(url)
			};
		}

		public ReminderStorage(HttpClient client)
		{
			_client = client;
		}

		public void Add(ReminderItem item)
		{
			var dto = new CreateReminderItemDto
			{
				Message = item.Message,
				ContactId = item.ContactId,
				DateTime = item.DateTime
			};
			var json = JsonSerializer.Serialize(dto);
			var content = new StringContent(json, Encoding.Default, "application/json");
			var response = _client.PostAsync($"/api/reminders/{item.Id}", content)
				.GetAwaiter()
				.GetResult();

			if (response.StatusCode == HttpStatusCode.Conflict)
			{
				throw new ReminderItemNotExistsException(item.Id);
			}

			response.EnsureSuccessStatusCode();
		}

		public void Update(ReminderItem item)
		{
			throw new NotImplementedException();
		}

		public void Delete(Guid id)
		{
			var response = _client.GetAsync($"/api/reminders/{id}")
				.GetAwaiter()
				.GetResult();

			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				throw new ReminderItemNotExistsException(id);
			}

			response = _client.DeleteAsync($"/api/reminders/{id}")
				.GetAwaiter()
				.GetResult();
		}

		public ReminderItem Get(Guid id)
		{
			var response = _client.GetAsync($"/api/reminders/{id}")
				.GetAwaiter()
				.GetResult();

			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				throw new ReminderItemNotExistsException(id);
			}

			response.EnsureSuccessStatusCode();

			var body = response.Content.ReadAsStringAsync()
				.GetAwaiter()
				.GetResult();

			var dto = JsonSerializer.Deserialize<ReminderItemDto>(body,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				}
			);
			return new ReminderItem(dto.Id, dto.Message, dto.ContactId, dto.DateTime, dto.Status);
		}

		public ReminderItem[] Find(DateTimeOffset? dateTime, ReminderItemStatus status)
		{
			var dto = new ForFind
			{
				DateTime = dateTime,
				Status = status
			};
			var json = JsonSerializer.Serialize(dto);
			var content = new StringContent(json, Encoding.Default, "application/json");
			var response = _client.PostAsync($"/api/reminders", content)
				.GetAwaiter()
				.GetResult();

			if (response.StatusCode == HttpStatusCode.Conflict)
			{
				throw new RemidersItemsNotExistsException();
			}
			
			var body = response.Content.ReadAsStringAsync()
				.GetAwaiter()
				.GetResult();

			var result = JsonSerializer.Deserialize<ReminderItem[]>(body,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				}
			);

			return result;
		}
	}
}