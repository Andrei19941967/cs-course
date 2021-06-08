using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace L21_C02_empty_asp_net_core_app_final
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
				.Build()
				.Run();
		}
	}

	public class Startup
	{
		public void Configure(IApplicationBuilder builder)
		{
			builder.UseRouting();
			builder.UseEndpoints(
				endpoints =>
				{
					endpoints.MapGet("/hello", context => WriteHtml(context, "Hello"));
					endpoints.MapGet("/world", context => WriteHtml(context, "World"));
					endpoints.MapPost("/post", context => WriteHtml(context, context.Request.Form["username"]));
				}
			);
		}

		private Task WriteHtml(HttpContext context, string content)
		{
			return context.Response.WriteAsync($"<html><body><h1>{content}</h1></body></html>");
		}
	}
}