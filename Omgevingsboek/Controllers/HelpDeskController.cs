using BusinessLogic.Services;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index()
        {
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
            bs.InsertVraag(resVraag);

            return View();
        }
        [Authorize(Roles = "Administrator,SuperAdministrator")]        
        public ActionResult IndexAdmin()
        {
            return View();
        }
    }
}