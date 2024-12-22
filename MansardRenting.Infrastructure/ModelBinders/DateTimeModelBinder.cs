using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace MansardRenting.Web.Infrastructure.ModelBinders
{
	/// <summary>
	/// Author: SomeOne
	/// Created: 22.12.2024
	/// Description: Корекция на датата
	/// </summary>
	public class DateTimeModelBinder : IModelBinder
	{
		#region Fields

		private readonly string _customFormat;

		#endregion

		public DateTimeModelBinder(string customFormat)
		{
			this._customFormat = customFormat;
		}

		Task IModelBinder.BindModelAsync(ModelBindingContext bindingContext)
		{
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);


			if (value != ValueProviderResult.None && !string.IsNullOrEmpty(value.FirstValue))
			{
				string valueAsString = value.FirstValue;

				bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);
				DateTime result = DateTime.MinValue;
				bool success = false;

				try
				{
					result = DateTime.ParseExact(valueAsString, _customFormat, CultureInfo.InvariantCulture);
					success = true;
				}
				catch (FormatException)
				{
					try
					{
						result = DateTime.Parse(valueAsString, new CultureInfo("bg-bg").DateTimeFormat);
						success = true;
					}
					catch (Exception e)
					{
						bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
					}
				}
				catch (Exception e)
				{
					bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
				}

				if (success)
				{
					bindingContext.Result = ModelBindingResult.Success(result);
					return Task.CompletedTask;
				}
			}
			return Task.CompletedTask;
		}
	}
}