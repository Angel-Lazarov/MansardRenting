﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MansardRenting.Web.Infrastructure.ModelBinders
{
	/// <summary>
	/// Author: SomeOne
	/// Created: 22.12.2024
	/// Description: Корекция на датата
	/// </summary>

	public class DateTimeModelBinderProvider : IModelBinderProvider
	{
		private readonly string _customFormat;

		public DateTimeModelBinderProvider(string dateFormat)
		{
			_customFormat = dateFormat;
		}

		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
			{
				return new DateTimeModelBinder(_customFormat);
			}

			return null!;
		}
	}
}