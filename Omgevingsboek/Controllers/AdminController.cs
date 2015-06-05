using BusinessLogic.Services;
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using Models.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
      
        [Authorize(Roles = "SuperAdministrator")]
        [HttpPost]
        public ActionResult HardDelete(ICollection<String> ActiviteitenToDelete, int vanaf, int desc, int filter)
        {
          // if (!User.IsInRole("SuperAdministrator")) return RedirectToAction("Activities");
          // foreach (int  activiteit in ActiviteitenToDelete)
          // {
          //     Activiteit a = bs.GetActiviteitById(activiteit);
          //     if (a == null) continue;
          //     bs.DeleteActiviteit(a);
          // }
            return RedirectToAction("Activities", "vanaf=" + vanaf + "&desc=" + desc + "&filter="+filter);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Delete(ICollection<int> ActiviteitenToDelete, int vanaf, int desc)
        {
            if (!User.IsInRole("SuperAdministrator") && !User.IsInRole("Administrator")) return RedirectToAction("Activities");
            foreach (int activiteit in ActiviteitenToDelete)
            {
                Activiteit a = bs.GetActiviteitById(activiteit);
                if (a == null) continue;
                bs.DeleteActiviteit(a);
                
            }
            return RedirectToAction("Activities", "vanaf=" + vanaf + "&desc=" + desc);
        }
        [HttpGet]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public ActionResult Activities(int? vanaf, int? desc, int? filter)
        {
            //desc == 1 -> descending
            //desc == 0 -> ascending

            List<Activiteit> res = new List<Activiteit>();

            if (!vanaf.HasValue) vanaf = 0;
            if (!desc.HasValue) desc = 0;
            if (!filter.HasValue) filter = 0;

            switch ((int)filter)
            {
                case 1:
                    //activiteit naam
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortNameZA((int)vanaf);
                    else
                        res = bs.GetActiviteiten50FromSortNameAZ((int)vanaf);
                    break;
                case 2:
                    //gebruiker naam
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortUserAZ((int)vanaf);
                    else
                        res = bs.GetActiviteiten50FromSortUserZA((int)vanaf);
                    break;
                case 3:
                    //poi
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortPoiAZ((int)vanaf);
                    else
                        res = bs.GetActiviteiten50FromSortPoiZA((int)vanaf);
                    break;
                default:
                    if (desc == 1)
                        res = bs.GetActiviteiten50FromSortNameZA((int)vanaf);
                    else
                        res = bs.GetActiviteiten50FromSortNameAZ((int)vanaf);
                    break;
            }
            ViewBag.vanaf = vanaf;
            ViewBag.desc = desc;
            ViewBag.filter = filter;

            return View(res);
        }
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        [HttpGet]
        public ActionResult Gebruikers(int? vanaf)
        {
            List<UserActivities> ua = new List<UserActivities>();
            if (!vanaf.HasValue) vanaf = 0;
            foreach(ApplicationUser user in bs.GetUserNext50((int)vanaf)){
                UserActivities u = new UserActivities();
                u.User = user;
                u.Activiteiten = bs.GetActivitiesByUsername(user.UserName);
                ua.Add(u);
            }

            return View(ua);
        }
        
        public ActionResult Boeken(int? vanaf)
        {
            if (!vanaf.HasValue) vanaf = 0;
            return View(bs.GetBoekNext50((int)vanaf));
        }
        
        public ActionResult Pois(int? vanaf)
        {
            if (!vanaf.HasValue) vanaf = 0;
            return View(bs.GetPoiNext50((int)vanaf));
        }
    }
}