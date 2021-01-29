﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web.Routing;
using K9.DataAccess.Attributes;
using K9.Globalisation;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Exceptions;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using WebMatrix.WebData;

namespace K9.DataAccess.Models
{
	public abstract class ObjectBase : IObjectBase, IValidatableObject
	{
		protected ObjectBase()
		{
			InitFileSources();
		}

		#region Properties

		private int _id;

		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id
		{
			get => _id;
		    set
			{
				if (_id != value)
				{
					_id = value;
					InitFileSources();
				}
			}
		}

		[Index(IsUnique = true)]
		[StringLength(128)]
		[Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.NameLabel)]
		public string Name { get; set; }

		[Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SystemStandard)]
		public bool IsSystemStandard { get; set; }

		[NotMapped]
		public bool IsSelected { get; set; }

		[Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DescriptionLabel)]
		public string Description => GetLocalisedDescription();

	    public string CreatePermissionName => GetCreatePermissionName();

	    public string EditPermissionName => GetEditPermissionName();

	    public string DeletePermissionName => GetDeletePermissionName();

	    public string ViewPermissionName => GetViewPermissionName();

	    #endregion


		#region Audit Fields

		[StringLength(255)]
		[Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CreatedByLabel)]
		public string CreatedBy { get; set; }

		[Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CreatedOnLabel)]
		public DateTime? CreatedOn { get; set; }

		[StringLength(255)]
		[Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LastUpdatedByLabel)]
		public string LastUpdatedBy { get; set; }

		[Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LastUpdatedOnLabel)]
		public DateTime? LastUpdatedOn { get; set; }

		#endregion


		#region Methods

		public string GetForeignKeyName()
		{
			return GetType().GetForeignKeyName();
		}

		private string GetCreatePermissionName()
		{
			return $"Create{GetType().Name}";
		}

		private string GetEditPermissionName()
		{
			return $"Edit{GetType().Name}";
		}

		private string GetDeletePermissionName()
		{
			return $"Delete{GetType().Name}";
		}

		private string GetViewPermissionName()
		{
			return $"View{GetType().Name}";
		}

		public string GetLocalisedDescription()
		{
			try
			{
				return typeof(Dictionary).GetValueFromResource(Name);
			}
			catch (Exception)
			{
			}

			return Name;
		}

		public void UpdateAuditFields()
		{
			var loggedinUser = "";

			try
			{
				loggedinUser = WebSecurity.CurrentUserName;
			}
			catch (Exception)
			{
				loggedinUser = SystemUser.System;
			}

			CreatedBy = loggedinUser;
			CreatedOn = DateTime.Now;
			LastUpdatedBy = loggedinUser;
			LastUpdatedOn = DateTime.Now;
		}

		public virtual void UpdateName() { }

		public RouteValueDictionary GetForeignKeyFilterRouteValues()
		{
			return new StatelessFilter(GetForeignKeyName(), Id).GetFilterRouteValues();
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			UpdateNameField();
			if (string.IsNullOrEmpty(this.GetProperty("Name").ToString()))
			{
				yield return new ValidationResult(Dictionary.FieldIsRequired, new[] { "Name" });
			}
		}

		public List<PropertyInfo> GetFileSourceProperties()
		{
			return GetType().GetProperties().Where(p => p.PropertyType == typeof(FileSource)).ToList();
		}

		private void UpdateNameField()
		{
			if (GetType().HasAttribute(typeof(AutoGenerateNameAttribute)))
			{
				Name = Guid.NewGuid().ToString();
			}
			else
			{
				UpdateName();
			}
		}

		private void InitFileSources()
		{
			var fileSourceProperties = this.GetFileSourceProperties();
			foreach (var propertyInfo in fileSourceProperties)
			{
				var fileSource = (FileSource)this.GetProperty(propertyInfo);
				if (fileSource == null)
				{
					fileSource = Activator.CreateInstance<FileSource>();
					this.SetProperty(propertyInfo, fileSource);
				}

				var info = propertyInfo.GetAttribute<FileSourceInfo>();
				if (info == null)
				{
					throw new FileSourceFilePathNotSpecifiedException();
				}
				fileSource.Filter = info.Filter;
				fileSource.PathToFiles = string.Join("/", info.PathToFiles, GetType().Name, Id.ToString());
			}
		}

		#endregion

	}
}
