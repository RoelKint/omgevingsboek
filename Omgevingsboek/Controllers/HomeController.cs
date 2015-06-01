using BusinessLogic.Repositories;
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
    public class HomeController : Controller
    {
        private IBoekService bs;

        public HomeController(IBoekService bs)
        {
            this.bs = bs;
        }

        [Authorize]
        public ActionResult Index()
        {

            HomeIndexPM hipm = new HomeIndexPM();
            hipm.BoekenEigenaar = bs.getBoekenByUser(User.Identity.Name);
            hipm.BoekenGedeeld = bs.getSharedBoeken(User.Identity.Name);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}