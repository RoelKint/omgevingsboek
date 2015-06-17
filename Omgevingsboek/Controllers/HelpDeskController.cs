using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Omgevingsboek.Controllers
{
    public class HelpDeskController : Controller
    {
        // GET: HelpDesk
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult PostVraag (Vraag vraag)
        {
            return View();
        }
        [Authorize(Roles = "Administrator,SuperAdministrator")]        
        public ActionResult IndexAdmin()
        {
            return View();
        }
    }
}