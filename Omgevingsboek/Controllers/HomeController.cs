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
        public ActionResult Gebruiker(String gebruikerId)
            {
            UserActivities ua = new UserActivities();
            ApplicationUser user = bs.GetUser(gebruikerId);
            ua.Activiteiten = bs.GetActivitiesByUsername(user.UserName);
            ua.Boeken = bs.GetBoekenByUser(user.UserName);
            ua.User = user;
            return View(ua);
        }
        public ActionResult Poi(String PoiId)
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
                pm.Activiteiten = bs.getActiviteitenPerPoi(poi.ID);
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