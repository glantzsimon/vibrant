﻿using System;
using System.Web.Mvc;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.ViewModels;
using NLog;

namespace K9.WebApplication.Controllers
{
	public class SupportController : BaseController
	{
		private readonly ILogger _logger;
		private readonly IMailer _mailer;
		private readonly WebsiteConfiguration _config;

		public SupportController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IMailer mailer, IOptions<WebsiteConfiguration> config)
			: base(logger, dataSetsHelper, roles)
		{
			_logger = logger;
			_mailer = mailer;
			_config = config.Value;
		}

		[HttpGet]
		public ActionResult ContactUs()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ContactUs(ContactUsViewModel model)
		{
			try
			{
				_mailer.SendEmail(
					model.Subject, 
					model.Body, 
					_config.SupportEmailAddress, 
					_config.CompanyName, 
					model.EmailAddress, 
					model.Name);

				return RedirectToAction("ContactUsSuccess");
			}
			catch (Exception ex)
			{
				_logger.Error(ex.GetFullErrorMessage());
				return View("FriendlyError");
			}
		}

		public ActionResult ContactUsSuccess()
		{
			return View();
		}
		
		public override string GetObjectName()
		{
			throw new System.NotImplementedException();
		}
	}
}
