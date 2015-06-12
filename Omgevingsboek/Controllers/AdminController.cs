using BusinessLogic.Services;
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using Models.PresentationModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Omgevingsboek.Controllers
{

    public class AdminController : Controller
    {
        private IBoekService bs;

        public AdminController(IBoekService bs)
        {
            this.bs = bs;
        }

        // GET: Admin
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Activities(int? vanaf, int? desc, int? filter, string search, int? mode)
        {
            //mode == 1 -> json
            //mode == 0/null -> view

            //desc == 1 -> descending
            //desc == 0 -> ascending
            if (search == null) search = "";
            List<Activiteit> res = new List<Activiteit>();

            if (!vanaf.HasValue) vanaf = 0;
            if (!desc.HasValue) desc = 0;
            if (!filter.HasValue) filter = 0;

            switch ((int)filter)
            {
                case 1:
                    //activiteit naam
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortNameZA((int)vanaf,search);
                    else
                        res = bs.GetActiviteiten50FromSortNameAZ((int)vanaf,search);
                    break;
                case 2:
                    //gebruiker naam
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortUserZA((int)vanaf, search);
                    else
                        res = bs.GetActiviteiten50FromSortUserAZ((int)vanaf, search);
                    break;
                case 3:
                    //poi
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortPoiZA((int)vanaf, search);
                    else
                        res = bs.GetActiviteiten50FromSortPoiAZ((int)vanaf, search);
                    break;
                default:
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortNameZA((int)vanaf, search);
                    else
                        res = bs.GetActiviteiten50FromSortNameAZ((int)vanaf, search);
                    break;

            }
            
            ViewBag.vanaf = vanaf;
            ViewBag.desc = desc;
            ViewBag.filter = filter;

            if((int) mode == 1)
                return Json(JsonConvert.SerializeObject(res), JsonRequestBehavior.AllowGet);
            else
            return View(res);
        }
        
        [Authorize(Roles = "SuperAdministrator")]
        [HttpPost]
        public ActionResult HardDelete(List<int> ActiviteitenToDelete, int vanaf, int desc, int? filter)
        {
            if (!User.IsInRole("SuperAdministrator")) return RedirectToAction("Activities");
            foreach (int activiteit in ActiviteitenToDelete)
            {
                Activiteit a = bs.GetActiviteitById(activiteit);
                if (a == null) continue;
                bs.DeleteActiviteit(a);
            }
            return RedirectToAction("Activities", new { vanaf = vanaf, desc = desc, filter = filter });
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Delete(List<int> ActiviteitenToDelete, int vanaf, int desc, int? filter)
        {
            if (!User.IsInRole("SuperAdministrator") && !User.IsInRole("Administrator")) return RedirectToAction("Activities");
            
            //TODO: ervoor zorgen dat lege gebruiker ook kan verwijderd worden.
            foreach (int activiteit in ActiviteitenToDelete)
            {
                Activiteit a = bs.GetActiviteitById(activiteit);
                if (a == null) continue;
                bs.DeleteActiviteit(a);
            }
            return RedirectToAction("Activities", new { vanaf = vanaf, desc = desc, filter = filter });
        }
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        [HttpGet]
        public ActionResult Gebruikers(int? vanaf, int? desc,string search, int? mode)
        {
            if (search == null) search = "";
            if (TempData["Feedback"] != null)
            {
                UitnodigingFeedbackPM fb = TempData["Feedback"] as UitnodigingFeedbackPM;
                if (fb.Foutief == null && fb.Gebruikt == null) ViewBag.IsFout = false;
                else
                {
                    ViewBag.IsFout = true;
                    ViewBag.Fouten = fb.Foutief;
                    ViewBag.Gebruikt = fb.Gebruikt;
                }
            }
            //mode == 1 -> json
            //mode == 0/null -> view

            //desc == 1 -> descending
            //desc == 0 -> ascending

            List<UserActivities> ua = new List<UserActivities>();
            List<ApplicationUser> res = new List<ApplicationUser>();

            if (!vanaf.HasValue) vanaf = 0;
            if (!desc.HasValue) desc = 0;


            if (desc == 1)
                res = bs.GetUserNext30SortZA((int)vanaf,search);
            else
                res = bs.GetUserNext30SortAZ((int)vanaf, search);

            foreach (ApplicationUser user in res)
            {
                UserActivities u = new UserActivities();
                u.User = user;
                u.Activiteiten = bs.GetActivitiesByUsername(user.UserName);
                ua.Add(u);
            }

            ViewBag.vanaf = vanaf;
            ViewBag.desc = desc;
            GebruikersPM gpm = new GebruikersPM();
            gpm.UserActivities = ua;
            gpm.Uitnodigingen = bs.GetUitnodigingenOpenByUser(User.Identity.Name);

            if ((int)mode == 1)
                return Json(JsonConvert.SerializeObject(gpm), JsonRequestBehavior.AllowGet);
            else
                return View(gpm);
        }

        public ActionResult ZoekGebruiker(string q)
        {
            //TODO: optimaliseren
            if (q == null) q = "";
            List<UserActivities> ua = new List<UserActivities>();
            List<ApplicationUser> geb = bs.GetUserNext30SortAZ(0,q);
            foreach (ApplicationUser user in geb)
            {
                UserActivities u = new UserActivities();
                u.User = user;
                u.Activiteiten = bs.GetActivitiesByUsername(user.UserName);
                ua.Add(u);
            }
            var jsonResult = Json(JsonConvert.SerializeObject(ua), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [Authorize(Roles = "Administrator,SuperAdministrator")]
        [HttpGet]
        public ActionResult Boeken(int? vanaf, int? desc, int? filter, string search, int? mode)
        {
            //mode == 1 -> json
            //mode == 0/null -> view

            //desc == 1 -> descending
            //desc == 0 -> ascending

            if (search == null) search = "";

            List<Boek> res = new List<Boek>();

            if (!vanaf.HasValue) vanaf = 0;
            if (!desc.HasValue) desc = 0;
            if (!filter.HasValue) filter = 0;

            switch ((int)filter)
            {
                case 1:
                    //boek naam
                    if (desc == 1)
                        res = bs.GetBoeken50FromSortNameZA((int)vanaf, search);
                    else
                        res = bs.GetBoeken50FromSortNameAZ((int)vanaf, search);
                    break;
                case 2:
                    //gebruiker naam
                    if (desc == 1)
                        res = bs.GetBoeken50FromSortUserZA((int)vanaf, search);
                    else
                        res = bs.GetBoeken50FromSortUserAZ((int)vanaf, search);
                    break;
                default:
                    if (desc == 1)
                        res = bs.GetBoeken50FromSortNameAZ((int)vanaf, search);
                    else
                        res = bs.GetBoeken50FromSortNameZA((int)vanaf, search);
                    break;
            }
            ViewBag.vanaf = vanaf;
            ViewBag.desc = desc;
            ViewBag.filter = filter;

            if ((int)mode == 1)
                return Json(JsonConvert.SerializeObject(res), JsonRequestBehavior.AllowGet);
            else
                return View(res);
        }
        

        [Authorize(Roles = "SuperAdministrator")]
        [HttpPost]
        public ActionResult HardDeleteBoeken(List<int> BoekenToDelete, int vanaf, int desc, int filter)
        {
            if (!User.IsInRole("SuperAdministrator")) return RedirectToAction("Activities");
            foreach (int boek in BoekenToDelete)
            {
                Boek a = bs.GetBoekByID(boek);
                if (a == null) continue;
                bs.DeleteBoek(a);
            }
            return RedirectToAction("Activities", new { vanaf = vanaf, desc = desc, filter = filter });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult DeleteBoeken(List<int> BoekenToDelete, int vanaf, int desc, int filter)
        {
            if (!User.IsInRole("SuperAdministrator") && !User.IsInRole("Administrator")) return RedirectToAction("Activities");
            foreach (int boek in BoekenToDelete)
            {
                Boek a = bs.GetBoekByID(boek);
                if (a == null) continue;
                bs.DeleteBoekSoft(a);
            }
            return RedirectToAction("Activities", new { vanaf = vanaf, desc = desc, filter = filter });
        }

        [Authorize(Roles = "Administrator,SuperAdministrator")]
        [HttpGet]
        public ActionResult Pois(int? vanaf, int? desc, string search, int? mode)
        {
            //mode 1 = json
            //mode 0/null = view

            //desc == 1 -> descending
            //desc == 0 -> ascending
            if (search == null) search = "";
            List<Poi> res = new List<Poi>();

            if (!vanaf.HasValue) vanaf = 0;
            if (!desc.HasValue) desc = 0;

            if (desc == 1)
                res = bs.GetPoi50FromSortNameZA((int)vanaf,search);
            else
                res = bs.GetPoi50FromSortNameAZ((int)vanaf, search);

            ViewBag.vanaf = vanaf;
            ViewBag.desc = desc;
            if((int) mode == 1)
                return Json(JsonConvert.SerializeObject(res), JsonRequestBehavior.AllowGet);
            else
                return View(res);
        }

        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult AddUsers(String Mails)
        {
            UitnodigingFeedbackPM fbPM = new UitnodigingFeedbackPM();
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                + "@"
                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            string[] Emails = new string[0];
            Emails = Mails.Split(new string[] { "\r\n", ",", " ",";" }, StringSplitOptions.None);

            foreach (string m in Emails)
            {
                if (regex.Match(m.Trim()).Success)
                {

                    if (bs.HeeftEmailAlEenUitnodiging(m.Trim()))
                    {
                        if (fbPM.Gebruikt == null) fbPM.Gebruikt = new List<string>();
                        fbPM.Gebruikt.Add(m.Trim());
                        continue;
                    }
                    Uitnodiging u = bs.CreateUitnodiging(User.Identity.Name, m.Trim());
                    ApplicationUser zenderNaam = bs.GetUser(User.Identity.Name);
                    UitnodigingSturen(m.Trim(), zenderNaam.Voornaam + " " + zenderNaam.Naam, u.Key);
                }
                else
                {
                    if (fbPM.Foutief == null) fbPM.Foutief = new List<string>();
                    fbPM.Foutief.Add(m.Trim());
                }
            }

            TempData["Feedback"] = fbPM;
            return RedirectToAction("Gebruikers");
        }

        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult HerzendUitnodiging(int? Id)
        {
            if (!Id.HasValue) return RedirectToAction("Gebruikers");
            Uitnodiging u = bs.GetUitnodigingById((int)Id);
            if (u == null) return RedirectToAction("Gebruikers");
            if (u.Gebruikt) return RedirectToAction("Gebruikers");
            ApplicationUser zenderNaam = bs.GetUser(u.Eigenaar.UserName);
            UitnodigingSturen(u.EmailUitgenodigde, zenderNaam.Voornaam + " " + zenderNaam.Naam, u.Key);

            return View();
        }


        public void UitnodigingSturen(string MailTo, string MailFrom, string Key)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", 587);

            System.Net.NetworkCredential creds = new System.Net.NetworkCredential("azure_9bab81a4769eae1b17dfaf2e69d71fd7@azure.com", "h8C2xnCHIEuESrt");

            smtpClient.Credentials = new System.Net.NetworkCredential("azure_9bab81a4769eae1b17dfaf2e69d71fd7@azure.com", "h8C2xnCHIEuESrt");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("azure_9bab81a4769eae1b17dfaf2e69d71fd7@azure.com", "Omgevingsboek Team");
            mail.To.Add(new MailAddress(MailTo));
            mail.Subject = "Uitnodiging voor het Omgevingsboek van " + MailFrom;
            mail.Body = "Beste,</br>" + MailFrom + " heeft je uitgenodigd om een account aan te maken op het omgevingsboek van Howest.</br>" +
            "Klik op onderstaande link om een account aan te maken. </br>" +
            "<a href=\"" + "http://localhost:44946/Account/register?Key=" + Key + "\"> http://localhost:44946/Account/register?Key=" + Key + " </a> </br> </br>" +
            "Met vriendelijke groeten, </br> Het Howest Omgevingsboek team.";

            
            smtpClient.Credentials = creds;
            smtpClient.Send(mail);
        }


        [Authorize(Roles = "SuperAdministrator")]
        [HttpPost]
        public ActionResult HardDeletePoi(List<int> PoisToDelete, int vanaf, int desc, int filter)
        {
            if (!User.IsInRole("SuperAdministrator")) return RedirectToAction("Activities");
            foreach (int poi in PoisToDelete)
            {
                Poi a = bs.GetPoiById(poi);
                if (a == null) continue;
                bs.DeletePoi(a);
            }
            return RedirectToAction("Activities", new { vanaf = vanaf, desc = desc, filter = filter });
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult DeletePoi(List<int> PoisToDelete, int vanaf, int desc, int filter)
        {
            if (!User.IsInRole("SuperAdministrator") && !User.IsInRole("Administrator")) return RedirectToAction("Activities");
            foreach (int poi in PoisToDelete)
            {
                Poi a = bs.GetPoiById(poi);
                if (a == null) continue;
                bs.DeletePoiSoft(a);
            }
            //return RedirectToAction("Activities", "vanaf=" + vanaf + "&desc=" + desc + "&filter=" + filter);
            return RedirectToAction("Activities", new { vanaf = vanaf, desc = desc, filter = filter });
        }



        [ChildActionOnly]
        public ActionResult GebrPartial()
        {
            /* List<PoiPM> poipms = new List<PoiPM>();
             List<Poi> pois = bs.GetPoiList();
             foreach (Poi poi in pois)
             {
                 PoiPM pm = new PoiPM()
                 {
                     poi = poi
                 };
                 pm.Activiteiten = bs.getActiviteitenPerPoi(poi.ID);
                 poipms.Add(pm);

             }*/
            return PartialView("_GebrPartial" /*, JsonConvert.SerializeObject(poipms)*/);
        }
    }

}