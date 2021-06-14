using System.ComponentModel.DataAnnotations;
using L21_C02_empty_asp_net_core_app_mvc.Attributes;

namespace L21_C02_empty_asp_net_core_app_mvc.ViewModels
{
	public class CityCreateViewModel
	{
		[Required]
		[MaxLength(128)]
		public string Name { get; set; }

		[Required]
		[MaxLength(1024)]
		[DifferentValue("Name")]
		public string Description { get; set; }

		[Range(100, 100_000_000)]
		public int Population { get; set; }
	}
}