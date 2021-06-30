using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Reminder.WebApi;

namespace Reminder.Storage.WebApi.Tests
{
	public class ReminderWebApplicationFactory : WebApplicationFactory<Startup>
	{
		private ReminderItem[] _existingItems = System.Array.Empty<ReminderItem>();

		public ReminderWebApplicationFactory WithExistingItems(params ReminderItem[] items)
		{
			_existingItems = items;
			return this;
		}

		public IReminderStorage CreateWebApiClient()
		{
			return new ReminderStorage(CreateClient());
		}

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
				{
					var descriptor = ServiceDescriptor.Singleton<IReminderStorage>(p =>
						new Memory.ReminderStorage(_existingItems)
					);
					services.Replace(descriptor);
				}
			);
		}
	}
}