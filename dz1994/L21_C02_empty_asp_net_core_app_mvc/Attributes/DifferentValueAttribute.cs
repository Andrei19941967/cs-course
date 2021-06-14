using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace L21_C02_empty_asp_net_core_app_mvc.Attributes
{
	public class DifferentValueAttribute : ValidationAttribute
	{
		public string OtherProperty { get; }

		public DifferentValueAttribute(string otherProperty)
		{
			OtherProperty = otherProperty;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var instanceType = validationContext.ObjectType;
			var propertyInfo = instanceType.GetRuntimeProperty(OtherProperty);
			if (propertyInfo == null)
			{
				return ValidationResult.Success;
			}

			var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance);
			if (Equals(value, propertyValue))
			{
				return new ValidationResult($"The value is same as property {OtherProperty}");
			}

			return ValidationResult.Success;
		}
	}
}