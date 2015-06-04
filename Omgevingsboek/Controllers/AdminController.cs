using BusinessLogic.Services;
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Activities()
        {

            return View(bs.GetActiviteitFirst50());
        }
        public ActionResult Activities(int? vanaf)
        {
            if (!vanaf.HasValue) return RedirectToAction("Activities");
            return View(bs.GetActiviteitNext50((int)vanaf));
        }
        public ActionResult Gebruikers()
        {
            return View(bs.GetUserFirst50());
        }
        public ActionResult Gebruikers(int? vanaf)
        {
            if (!vanaf.HasValue) return RedirectToAction("Gebruikers");
            return View(bs.GetUserNext50((int)vanaf));
        }
        public ActionResult Boeken()
        {
            return View(bs.GetBoekFirst50());
        }
        public ActionResult Boeken(int? vanaf)
        {
            if (!vanaf.HasValue) return RedirectToAction("Boeken");
            return View(bs.GetBoekNext50((int)vanaf));
        }
        public ActionResult Pois()
        {
            return View(bs.GetPoiFirst50());
        }
        public ActionResult Pois(int? vanaf)
        {
            if (!vanaf.HasValue) return RedirectToAction("Pois");
            return View(bs.GetPoiNext50((int)vanaf));
        }
    }
}