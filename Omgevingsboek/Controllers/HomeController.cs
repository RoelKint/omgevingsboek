﻿using BusinessLogic.Repositories;
using BusinessLogic.Services;
using FlickrNet;
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using Models.PresentationModels;
using Newtonsoft.Json;
using Omgevingsboek.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Omgevingsboek.Controllers
{
    public class HomeController : Controller
    {
        private IBoekService bs;
        private Flickr flickr;

        public HomeController(IBoekService bs)
        {
            this.bs = bs;
            flickr = MvcApplication.flickr;
            if (flickr == null) flickr = FlickrApiManager.GetInstance();
        }

        [Authorize]
        public ActionResult Gebruiker(String gebruikerId,int? vanafActiviteit, int? vanafBoek )
        {
            if (!vanafActiviteit.HasValue) vanafActiviteit = 0;
            if (!vanafBoek.HasValue) vanafBoek = 0;

            UserActivities ua = new UserActivities();
            ApplicationUser user = bs.GetUser(gebruikerId);
            ua.Activiteiten = bs.getActiviteitenUserByUser50from((int) vanafActiviteit, user.UserName, User.Identity.Name);
            ua.Boeken = bs.getBoekUserByUser50from((int) vanafBoek, user.UserName, User.Identity.Name);
            ua.User = user;
            return View(ua);
        }

        public ActionResult Poi(int PoiId)
        {
            
            return View();
        }
        
        [Authorize]
        public ActionResult Index()
        {
            HomeIndexPM hipm = new HomeIndexPM();
            hipm.BoekenEigenaar = bs.GetBoekenByUser(User.Identity.Name);
            hipm.BoekenGedeeld = bs.GetSharedBoeken(User.Identity.Name);

            return View(hipm);
        }
        [Authorize]
        public ActionResult Boek(int? Id)
        {
            //activities zitten erin
            if (!Id.HasValue) return RedirectToAction("Index");
            Boek boek = bs.GetBoekByID((int)Id);
            if (boek == null) return RedirectToAction("Index");
            if (boek.Eigenaar.UserName != User.Identity.Name || !boek.DeelLijst.Contains(bs.GetUser(User.Identity.Name))) return RedirectToAction("Index");

            return View(boek);
        }

        public ActionResult Activiteit(int? Id)
        {
            if (!Id.HasValue) return RedirectToAction("Index");
            Activiteit activiteit = bs.GetActiviteitById((int)Id);
            if (activiteit == null) return RedirectToAction("Index");
            ApplicationUser user = bs.GetUser(User.Identity.Name);
            if (activiteit.Eigenaar.UserName != User.Identity.Name || !activiteit.DeelLijst.Contains<ApplicationUser>(user)) return RedirectToAction("Index");
            
            return View(activiteit);
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

        public ActionResult Test()
        {
            
            return View();
        }

        [ChildActionOnly]
        public ActionResult PoiPartial()
        {
            List<PoiPM> poipms = new List<PoiPM>();
            List<Poi> pois = bs.GetPoiList();
            foreach(Poi poi in pois){
                PoiPM pm = new PoiPM(){
                    poi = poi
                };
                pm.Activiteiten = bs.getActiviteitenByPoiByUser50from(0, User.Identity.Name, poi.ID);
                poipms.Add(pm);

            }
            return PartialView("_PoiPartial",JsonConvert.SerializeObject(poipms));
        }

        [HttpPost]
        public ActionResult Test(HttpPostedFileBase picture)
        {
            String a = flickr.UploadPicture(picture.InputStream, "test", "test", "test", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden);
            
            flickr.PhotosetsAddPhoto(ConfigurationManager.AppSettings.Get("FlickrBoekCoverId"), a);
            

            return View();
        }
        public ActionResult GetTags()
        {
            List<Models.OmgevingsBoek_Models.Tag> tags = bs.GetTagList();
            List<SimpleTag> stl = new List<SimpleTag>();
            foreach (Models.OmgevingsBoek_Models.Tag tag in tags)
            {
                stl.Add(new SimpleTag()
                {
                    Id = tag.ID,
                    Naam = tag.Naam
                });
            }
            return Json(JsonConvert.SerializeObject(stl), JsonRequestBehavior.AllowGet);
        }
    }
}