using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reminder.Storage;
using Reminder.Storage.Memory;
using Reminder.WebApi.Middlewares;

namespace Reminder.WebApi
{
	public class Startup
	{
		// Dependency Injection
		// <Interface> <Implementation>
		// <IReminderStorage> <ReminderStorage (memory)>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			// services.AddSingleton()
			// services.AddTransient()
			// services.AddScoped()
			services.AddSingleton<IReminderStorage, ReminderStorage>(_ =>
				new ReminderStorage(
					new ReminderItem("One reminder", "contact 2", DateTimeOffset.UtcNow.AddMinutes(-120)),
					new ReminderItem("Two reminder", "contact 1", DateTimeOffset.UtcNow.AddMinutes(2))
				)
			);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseMiddleware<ExceptionHandlingMiddleware>();
			app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
		}
	}
}