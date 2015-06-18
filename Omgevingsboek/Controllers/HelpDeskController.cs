﻿using BusinessLogic.Services;
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

    }
}
