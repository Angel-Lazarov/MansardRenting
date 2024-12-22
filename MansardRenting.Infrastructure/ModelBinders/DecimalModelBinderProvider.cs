using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MansardRenting.Web.Infrastructure.ModelBinders
{
	/// <summary>
	/// Author: SomeOne
	/// Created: 22.12.2024
	/// Description: Корекция на десетичната запетая
	/// </summary>
	public class DecimalModelBinderProvider : IModelBinderProvider
	{
		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (context.Metadata.ModelType == typeof(decimal) ||
				context.Metadata.ModelType == typeof(decimal?))
			{
				return new DecimalModelBinder();
			}

			return null!;
		}
	}
}
