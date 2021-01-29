using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using K9.SharedLibrary.Models;
using Microsoft.Ajax.Utilities;
using NLog;

namespace K9.WebApplication.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles)
            : base(logger, dataSetsHelper, roles)
        {
        }

        public ActionResult Index()
        {
            return Content("This website is currently under development.");
        }

        [Route("calendar")]
        public ActionResult GetCalendar(string url, string key)
        {
            return Redirect($"{url}&key={key}&ms={DateTime.Now.Millisecond}");
        }

        public ActionResult SetLanguage(string languageCode)
        {
            Session["languageCode"] = languageCode;
            return RedirectToAction("Index");
        }

        public override string GetObjectName()
        {
            return string.Empty;
        }
    }
}
