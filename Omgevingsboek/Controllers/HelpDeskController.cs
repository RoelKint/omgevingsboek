using BusinessLogic.Services;
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Omgevingsboek.Controllers
{
    public class HelpDeskController : Controller
    {
        IBoekService bs = null;

        public HelpDeskController(IBoekService bs)
        {
            this.bs = bs;
        }

        // GET: HelpDesk
        [Authorize]

        public ActionResult Index()
        {
            ViewBag.vragen = bs.GetVragenByUser(User.Identity.Name);
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult PostVraag (Vraag vraag)
        {
            Vraag resVraag = new Vraag()
            {
                Omschrhijving = vraag.Omschrhijving,
                Titel = vraag.Titel,
                Datum = DateTime.Now,
                EigenaarId = bs.GetUser(User.Identity.Name).Id
            };
            Vraag antw = bs.InsertVraag(resVraag);

            if (antw.Id > 0) return View("Success");
            return RedirectToAction("index");
        }
        [Authorize(Roles = "Administrator,SuperAdministrator")]        
        public ActionResult IndexAdmin(string filter)
        {
            
            ViewBag.filter = filter;
            return View(bs.GetVragen(filter));
        }
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public string GelezenTicket(int Id)
        {
            if (bs.GetVraagByID(Id) == null) return "NOK";
            bs.VraagGelezen(Id);
            return "OK";
        }
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public string VerwijderTicket(int Id)
        {
            if (bs.GetVraagByID(Id) == null) return "NOK";
            bs.VraagOpgelost(Id);
            return "OK";
        }
        [Authorize(Roles = "Administrator,SuperAdministrator")]        
        public ActionResult Antwoord(int? Id)
        {
            if (!Id.HasValue) return RedirectToAction("IndexAdmin");
            Vraag v = bs.GetVraagByID((int)Id);
            if (v == null) return RedirectToAction("IndexAdmin");
            
            return View(v);
        }
        
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult VerstuurAntwoord(int? Id,string text)
        {
            if (!Id.HasValue) return RedirectToAction("IndexAdmin");
            Vraag v = bs.GetVraagByID((int)Id);
            if (v == null) return RedirectToAction("IndexAdmin");
            
            ApplicationUser admin = bs.GetUser(User.Identity.Name);
            text = text.Replace("\r\n", "<br/>");
            AntwoordSturen(v.Eigenaar.UserName, admin.Voornaam + " " + admin.Naam, text, v.Titel);

            bs.VraagGelezen(v.Id);
            return RedirectToAction("IndexAdmin", new { filter = "gelezen" });
        }
        [NonAction]
        public void AntwoordSturen(string MailTo, string MailFrom, string body, string titel)
        {
            SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings.Get("SMTPServer"), int.Parse(ConfigurationManager.AppSettings.Get("SMTPPoort")));

            System.Net.NetworkCredential creds = new System.Net.NetworkCredential(ConfigurationManager.AppSettings.Get("SMTPUserName"), ConfigurationManager.AppSettings.Get("SMTPPasswoord"));

            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings.Get("SMTPUserName"), ConfigurationManager.AppSettings.Get("SMTPPasswoord"));
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();


            mail.IsBodyHtml = true;
            //Admin address meschien nog veranderen??
            mail.From = new MailAddress(ConfigurationManager.AppSettings.Get("SMTPUserName"), "Omgevingsboek Team: " + MailFrom);
            mail.To.Add(new MailAddress(MailTo));
            mail.Subject = "Antwoord op uw vraag: "+titel;
            mail.Body =  body+
            "Met vriendelijke groeten, "+ MailFrom +".";


            smtpClient.Credentials = creds;
            smtpClient.Send(mail);
        }
    }
}
