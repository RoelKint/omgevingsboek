using BusinessLogic.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using Models.PresentationModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            Session["stap1"] = "Admin";
            Session["url1"] = "../admin";
            Session.Remove("stap3");
            Session.Remove("stap2");
            ViewBag.stap1 = Session["stap1"];
            ViewBag.url1 = Session["url1"];
            return View();

        }


        #region Activiteiten


        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Activities(int? vanaf, int? desc, int? filter, string search, int? mode)
        {
            Session.Remove("stap3");
            Session["stap2"] = "activities";
            Session["url2"] = "/../admin/Activities";
            ViewBag.stap1 = Session["stap1"];
            ViewBag.url1 = Session["url1"];
            ViewBag.stap2 = Session["stap2"];
            ViewBag.url2 = Session["url2"];
            //mode == 1 -> json
            //mode == 0/null -> view

            //desc == 1 -> descending
            //desc == 0 -> ascending
            if (search == null) search = "";
            List<Activiteit> res = new List<Activiteit>();
            bool DisplayDeleted = false;
            if (bs.GetUser(User.Identity.Name).Roles.Any(r => r.RoleId == "95311bc7-8180-4c53-9e33-61fd254c21fc")) DisplayDeleted = true;


            if (!vanaf.HasValue) vanaf = 0;
            if (!desc.HasValue) desc = 0;
            if (!filter.HasValue) filter = 0;

            switch ((int)filter)
            {
                case 1:
                    //activiteit naam
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortNameZA((int)vanaf,search,DisplayDeleted);
                    else
                        res = bs.GetActiviteiten50FromSortNameAZ((int)vanaf, search, DisplayDeleted);
                    break;
                case 2:
                    //gebruiker naam
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortUserZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetActiviteiten50FromSortUserAZ((int)vanaf, search, DisplayDeleted);
                    break;
                case 3:
                    //poi
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortPoiZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetActiviteiten50FromSortPoiAZ((int)vanaf, search, DisplayDeleted);
                    break;
                case 4:
                    //poi
                    if (desc == 1)
                        res = bs.getActiviteiten50FromSortDeletedZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.getActiviteiten50FromSortDeletedAZ((int)vanaf, search, DisplayDeleted);
                    break;
                default:
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortNameZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetActiviteiten50FromSortNameAZ((int)vanaf, search, DisplayDeleted);
                    break;
            }
            
            ViewBag.vanaf = vanaf;
            ViewBag.desc = desc;
            ViewBag.filter = filter;

            if (!mode.HasValue || (int)mode == 0)
                return View(res);
            else
                return Json(JsonConvert.SerializeObject(res), JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "SuperAdministrator")]
        [HttpPost]
        public String HardDeleteActiviteit(List<int> ActiviteitenToDelete, int vanaf, int desc, int? filter)
        {
            if (!User.IsInRole("SuperAdministrator")) return "fail";
            foreach (int activiteit in ActiviteitenToDelete)
            {
                Activiteit a = bs.GetActiviteitByIdAdmin(activiteit);
                if (a == null) return "fail";
                bs.DeleteActiviteit(a);
            }
            return "ok";
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public String DeleteActiviteit(List<int> ActiviteitenToDelete, int vanaf, int desc, int? filter)
        {
            if (!User.IsInRole("SuperAdministrator") && !User.IsInRole("Administrator")) return "fail";
            
            //TODO: ervoor zorgen dat lege gebruiker ook kan verwijderd worden.
            foreach (int activiteit in ActiviteitenToDelete)
            {
                Activiteit a = bs.GetActiviteitByIdAdmin(activiteit);
                if (a == null) return "fail";
                bs.DeleteActiviteitSoft(a);
            }
            return "ok";
        }

        #endregion


        #region Gebruikers

        [Authorize(Roles = "Administrator,SuperAdministrator")]
        [HttpGet]
        public ActionResult Gebruikers(int? vanaf, int? desc,string search, int? mode,int? filter)
        {
            Session.Remove("stap3");
            Session["stap2"] = "Gebruikers";
            Session["url2"] = "/../admin/gebruikers";
            ViewBag.stap1 = Session["stap1"];
            ViewBag.url1 = Session["url1"];
            ViewBag.stap2 = Session["stap2"];
            ViewBag.url2 = Session["url2"];
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
            bool DisplayDeleted = false;
            if (bs.GetUser(User.Identity.Name).Roles.Any(r => r.RoleId == "95311bc7-8180-4c53-9e33-61fd254c21fc")) DisplayDeleted = true;

            List<UserActivities> ua = new List<UserActivities>();
            List<ApplicationUser> res = new List<ApplicationUser>();

            if (!filter.HasValue) filter = 0;
            if (!vanaf.HasValue) vanaf = 0;
            if (!desc.HasValue) desc = 0;

            switch ((int)filter)
            {
                case 1:
                    if (desc == 1)
                        res = bs.GetUserNext30SortNaamZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetUserNext30SortNaamAZ((int)vanaf, search, DisplayDeleted);
                    break;
                case 2:
                    if (desc == 1)
                        res = bs.GetUserNext30SortEmailZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetUserNext30SortEmailAZ((int)vanaf, search, DisplayDeleted);
                    break;
                case 4:
                    if (desc == 1)
                        res = bs.GetUserNext30SortRoleZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetUserNext30SortRoleAZ((int)vanaf, search, DisplayDeleted);
                    break;
                case 5:
                    if (desc == 1)
                        res = bs.GetUserNext30SortDeletedZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetUserNext30SortDeletedAZ((int)vanaf, search, DisplayDeleted);
                    break;
                default:
                    if (desc == 1)
                        res = bs.GetUserNext30SortNaamZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetUserNext30SortNaamAZ((int)vanaf, search, DisplayDeleted);
                    break;
            }


            foreach (ApplicationUser user in res)
            {
                UserActivities u = new UserActivities();
                u.User = user;
                u.Activiteiten = bs.GetActivitiesByUsername(user.UserName);
                if (user.Roles.Any(x => x.RoleId == "a2727df4-4163-4442-9b81-ba018f6ff99a")) u.Role = "User";
                if (user.Roles.Any(x => x.RoleId == "0e17317e-9f44-4117-b033-4c5c7c2217fe")) u.Role = "Administrator";
                if (user.Roles.Any(x => x.RoleId == "95311bc7-8180-4c53-9e33-61fd254c21fc")) u.Role = "SuperAdministrator";

                ua.Add(u);
            }


            ViewBag.vanaf = vanaf;
            ViewBag.desc = desc;
            ViewBag.filter = filter;
            GebruikersPM gpm = new GebruikersPM();
            gpm.UserActivities = ua;
            gpm.Uitnodigingen = bs.GetUitnodigingenOpenByUser(User.Identity.Name);
            
            if (!mode.HasValue || (int)mode == 0)
                return View(gpm);
            else
            {
                var jsonResult = Json(JsonConvert.SerializeObject(ua), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        

        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult AddUsers(String Mails)
        {
            UitnodigingFeedbackPM fbPM = new UitnodigingFeedbackPM();
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                + "@"
                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            string[] Emails = new string[0];
            Emails = Mails.Split(new string[] { "\r\n", ",", " ", ";" }, StringSplitOptions.None);

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
        [Authorize(Roles = "SuperAdministrator")]
        public ActionResult DeleteUsersHard(List<string> UsersToDelete, int vanaf, int desc, int? filter)
        {
            if (!User.IsInRole("SuperAdministrator")) return RedirectToAction("Users");

            //TODO: ervoor zorgen dat lege gebruiker ook kan verwijderd worden.
            foreach (string UserId in UsersToDelete)
            {
                ApplicationUser u = bs.GetUserById(UserId);
                if (u == null) continue;
                bs.DeleteUserHard(u);
            }
            return RedirectToAction("Activities", new { vanaf = vanaf, desc = desc, filter = filter });
        }
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult DeleteUsersSoft(List<string> UsersToDelete, int vanaf, int desc, int? filter)
        {
            if (!User.IsInRole("SuperAdministrator") && !User.IsInRole("Administrator")) return RedirectToAction("Users");

            //TODO: ervoor zorgen dat lege gebruiker ook kan verwijderd worden.
            foreach (string UserId in UsersToDelete)
            {
                ApplicationUser u = bs.GetUserById(UserId);
                if (u == null) continue;
                bs.DeleteUserSoft(u);
            }
            return RedirectToAction("Activities", new { vanaf = vanaf, desc = desc, filter = filter });
        }

        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public string HerzendUitnodiging(int? Id)
        {
            if (!Id.HasValue) return "NOK";
            Uitnodiging u = bs.GetUitnodigingById((int)Id);
            if (u == null) return "NOK";
            if (u.Gebruikt) return "NOK";
            ApplicationUser zenderNaam = bs.GetUser(u.Eigenaar.UserName);
            UitnodigingSturen(u.EmailUitgenodigde, zenderNaam.Voornaam + " " + zenderNaam.Naam, u.Key);

            return "OK";
        }

        [NonAction]
        public void UitnodigingSturen(string MailTo, string MailFrom, string Key)
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
            mail.From = new MailAddress(ConfigurationManager.AppSettings.Get("SMTPUserName"), "Omgevingsboek Team");
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
        public string ToggeRole(List<string> UsersNames)
        {
            //TODO: checks
            foreach (string user in UsersNames)
            {
                if (bs.GetUser(user) == null) return "NOK";
                bs.ToggleRole(user);

            }
            return "OK";
        }        


        #endregion


        #region Boeken


        [Authorize(Roles = "Administrator,SuperAdministrator")]
        [HttpGet]
        public ActionResult Boeken(int? vanaf, int? desc, int? filter, string search, int? mode)
        {
            Session.Remove("stap3");
            Session["stap2"] = "Boeken";
            Session["url2"] = "/../admin/boeken";
            ViewBag.stap1 = Session["stap1"];
            ViewBag.url1 = Session["url1"];
            ViewBag.stap2 = Session["stap2"];
            ViewBag.url2 = Session["url2"];
            //mode == 1 -> json
            //mode == 0/null -> view

            //desc == 1 -> descending
            //desc == 0 -> ascending
            bool DisplayDeleted = false;
            if (bs.GetUser(User.Identity.Name).Roles.Any(r => r.RoleId == "95311bc7-8180-4c53-9e33-61fd254c21fc")) DisplayDeleted = true;

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
                        res = bs.GetBoeken50FromSortNameZA((int)vanaf, search,DisplayDeleted);
                    else
                        res = bs.GetBoeken50FromSortNameAZ((int)vanaf, search, DisplayDeleted);
                    break;
                case 2:
                    //gebruiker naam
                    if (desc == 1)
                        res = bs.GetBoeken50FromSortUserZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetBoeken50FromSortUserAZ((int)vanaf, search, DisplayDeleted);
                    break;
                case 4:
                    if (desc == 1)
                        res = bs.GetBoeken50FromSortDeletedZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetBoeken50FromSortDeletedAZ((int)vanaf, search, DisplayDeleted);
                    break;
                default:
                    if (desc == 1)
                        res = bs.GetBoeken50FromSortNameZA((int)vanaf, search, DisplayDeleted);
                    else
                        res = bs.GetBoeken50FromSortNameAZ((int)vanaf, search, DisplayDeleted);
                    break;
            }
            ViewBag.vanaf = vanaf;
            ViewBag.desc = desc;
            ViewBag.filter = filter;

            if (!mode.HasValue || (int)mode == 0)
                return View(res);
            else
                return Json(JsonConvert.SerializeObject(res), JsonRequestBehavior.AllowGet);
        }



        [Authorize(Roles = "SuperAdministrator")]
        [HttpPost]
        public String HardDeleteBoeken(List<int> BoekenToDelete, int vanaf, int desc, int filter)
        {
            if (!User.IsInRole("SuperAdministrator")) return "fail";
            foreach (int boek in BoekenToDelete)
            {
                Boek a = bs.GetBoekByIDAdmin(boek);
                if (a == null) return "fail";
                bs.DeleteBoek(a);
            }
            return "ok";

        }

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public String DeleteBoeken(List<int> BoekenToDelete, int vanaf, int desc, int filter)
        {
            if (!User.IsInRole("SuperAdministrator") && !User.IsInRole("Administrator")) return "fail";
            foreach (int boek in BoekenToDelete)
            {
                Boek a = bs.GetBoekByIDAdmin(boek);
                if (a == null) return "fail";
                bs.DeleteBoekSoft(a);
            }
            return "ok";
        }

        #endregion


        #region Pois

        [Authorize(Roles = "Administrator,SuperAdministrator")]
        [HttpGet]
        public ActionResult Pois(int? vanaf, int? desc, string search, int? filter, int? mode)
        {
            Session.Remove("stap3");
            Session["stap2"] = "Pois";
            Session["url2"] = "/../admin/pois";
            ViewBag.stap1 = Session["stap1"];
            ViewBag.url1 = Session["url1"];
            ViewBag.stap2 = Session["stap2"];
            ViewBag.url2 = Session["url2"];
            //mode 1 = json
            //mode 0/null = view

            //desc == 1 -> descending
            //desc == 0 -> ascending

            bool DisplayDeleted = false;
            if (bs.GetUser(User.Identity.Name).Roles.Any(r => r.RoleId == "95311bc7-8180-4c53-9e33-61fd254c21fc")) DisplayDeleted = true;

            if (search == null) search = "";
            List<Poi> res = new List<Poi>();
            if (!filter.HasValue) filter = 0;
            if (!vanaf.HasValue) vanaf = 0;
            if (!desc.HasValue) desc = 0;
        switch ((int)filter)
            {
            case 1:
                //naam
                if (desc == 1)
                    res = bs.GetPoi50FromSortNameZA((int)vanaf,search,DisplayDeleted);
                else
                    res = bs.GetPoi50FromSortNameAZ((int)vanaf, search, DisplayDeleted);
            break;
            case 2:
                //email
                if (desc == 1)
                    res = bs.getPoi50FromSortEmailZA((int)vanaf, search, DisplayDeleted);
                else
                    res = bs.getPoi50FromSortEmailAZ((int)vanaf, search, DisplayDeleted);
            break;
            case 3:
                //address
                if (desc == 1)
                    res = bs.getPoi50FromSortAddressZA((int)vanaf, search, DisplayDeleted);
                else
                    res = bs.getPoi50FromSortAddressAZ((int)vanaf, search, DisplayDeleted);
            break;
            case 6:
            //address
            if (desc == 1)
                res = bs.getPoi50FromSortDeletedZA((int)vanaf, search, DisplayDeleted);
            else
                res = bs.getPoi50FromSortDeletedAZ((int)vanaf, search, DisplayDeleted);
            break;
            default:
                if (desc == 1)
                    res = bs.GetPoi50FromSortNameZA((int)vanaf, search, DisplayDeleted);
                else
                    res = bs.GetPoi50FromSortNameAZ((int)vanaf, search, DisplayDeleted);
            break;
            }

            ViewBag.vanaf = vanaf;
            ViewBag.desc = desc;
            ViewBag.filter = filter;
           if (!mode.HasValue || (int)mode == 0)
                return View(res);
            else
                return Json(JsonConvert.SerializeObject(res), JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "SuperAdministrator")]
        [HttpPost]
        public String HardDeletePoi(List<int> PoisToDelete, int vanaf, int desc, int filter)
        {
            if (!User.IsInRole("SuperAdministrator")) return "fail";
            foreach (int poi in PoisToDelete)
            {
                Poi a = bs.GetPoiByIdAdmin(poi);
                if (a == null) return "fail";
                bs.DeletePoi(a);
            }
            return "ok";
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public String DeletePoi(List<int> PoisToDelete, int vanaf, int desc, int filter)
        {
            if (!User.IsInRole("SuperAdministrator") && !User.IsInRole("Administrator")) return "fail";
            foreach (int poi in PoisToDelete)
            {
                Poi a = bs.GetPoiByIdAdmin(poi);
                if (a == null) return "fail";
                bs.DeletePoiSoft(a);
            }
            return "ok";
        }

        #endregion


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