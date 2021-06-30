using System;
using Microsoft.AspNetCore.Mvc;
using Reminder.Storage;
using Reminder.WebApi.ViewModels;

namespace Reminder.WebApi.Controllers
{
	[Route("/api/reminders")]
	public class ReminderController : ControllerBase
	{
		private readonly IReminderStorage _storage;

		public ReminderController(IReminderStorage storage)
		{
			_storage = storage;
		}

		[HttpGet]
		public IActionResult Find(
			[FromQuery] DateTimeOffset? dateTime,
			[FromQuery] ReminderItemStatus status)
		{
			if (!Enum.IsDefined(status))
			{
				return BadRequest();
			}

			var items = _storage.Find(dateTime, status);

			return Ok(items);
		}

		[HttpGet("{id:guid}")]
		public IActionResult Get(Guid id)
		{
			var item = _storage.Get(id);
			return Ok(item);
		}

		[HttpPost("{id?}")]
		public IActionResult Add(Guid? id, [FromBody] CreateReminderViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var item = new ReminderItem(
				id ?? Guid.NewGuid(),
				model.Message.Trim(),
				model.ContactId.Trim(),
				model.DateTime,
				ReminderItemStatus.Created
			);

			_storage.Add(item);

			return CreatedAtAction("Get", new {id = item.Id}, item);
		}

		[HttpDelete("{id:guid}")]
		public IActionResult Delete(Guid id)
		{
			_storage.Delete(id);
			return NoContent();
		}

		[HttpPatch("{id:guid}")]
		public IActionResult Update(Guid id, [FromBody] UpdateReminderViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var item = _storage.Get(id);

			item = new ReminderItem(
				id,
				model.Message,
				item.ContactId,
				item.DateTime,
				model.Status
			);

			_storage.Update(item);

			return Ok(item);
		}
	}
}