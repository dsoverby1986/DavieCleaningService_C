using DavieCleaningService__C.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DavieCleaningService__C.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace DavieCleaningService__C.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.GoogleMapAPIKey = ConfigurationManager.AppSettings["GoogleMapsAPIKey"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Contact(ContactMessage message)
        {
            var apiKey = ConfigurationManager.AppSettings["DCS_SendGrid_API_Key"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(message.FromEmail, "DCS Web App");
            var subject = message.Subject;
            var to = new EmailAddress(ConfigurationManager.AppSettings["InfoEmail"]);
            var plainTextContent = message.FormatBody().Message;
            var email = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, null);
            var response = await client.SendEmailAsync(email);
            return Json(new { fail = response.StatusCode != HttpStatusCode.Accepted }, JsonRequestBehavior.AllowGet);
        }
    }
}