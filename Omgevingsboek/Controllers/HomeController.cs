using BusinessLogic.Repositories;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Omgevingsboek.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ActiviteitRepository repo = new ActiviteitRepository();

            List<Activiteit> lst = repo.getSharedActivitiesByUsername("testSuperAdmin@howest.be");
            List<Activiteit> lsts = repo.getSharedActivitiesByBookId(1,"testSuperAdmin@howest.be");

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