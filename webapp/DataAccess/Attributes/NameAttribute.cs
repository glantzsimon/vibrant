﻿using System;
using K9.SharedLibrary.Extensions;

namespace K9.DataAccess.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class NameAttribute : Attribute
	{
		public string Name { get; set; }
		public string PluralName { get; set; }
		public string ListName { get; set; }
		public Type ResourceType { get; set; }

		public string GetName()
		{
			return ResourceType.GetValueFromResource(Name);
		}

		public string GetPluralName()
		{
			return string.IsNullOrEmpty(PluralName) ? $"{GetName()}s" : ResourceType.GetValueFromResource(PluralName);
		}

		public string GetListName()
		{
			return string.IsNullOrEmpty(ListName) ? $"{Name}s" : ListName;
		}
	}
}
