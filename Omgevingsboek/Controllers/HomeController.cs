using BusinessLogic.Repositories;
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
        public List<string> FlickrPhotoURLs = new List<string>();
        
        public event EventHandler FlickrPhotoURLsLoaded;

        public HomeController(IBoekService bs)
        {
            this.bs = bs;
            flickr = MvcApplication.flickr;

        }

        [Authorize]
        public ActionResult Index()
        {
            HomeIndexPM hipm = new HomeIndexPM();
            hipm.BoekenEigenaar = bs.GetBoekenByUser(User.Identity.Name);
            hipm.BoekenGedeeld = bs.GetSharedBoeken(User.Identity.Name);
            hipm.FotoIds = new List<FotoId>();
            foreach (Boek boek in hipm.BoekenEigenaar)
            {
                FotoId id = new FotoId()
                {
                    Id = boek.Id
                };
                if (boek.Afbeelding != null && flickr != null)
                    try
                    {
                        id.PhotoUrl = flickr.PhotosGetInfo(boek.Afbeelding).MediumUrl;
                    }
                    catch (FlickrNet.Exceptions.PhotoNotFoundException ex)
                    {
                        
                        id.PhotoUrl = null;
                    }
                hipm.FotoIds.Add(id);
            }
            foreach (Boek boek in hipm.BoekenGedeeld )
            {
                FotoId id = new FotoId()
                {
                    Id = boek.Id
                };
                if (boek.Afbeelding != null && flickr != null)
                    try
                    {
                        id.PhotoUrl = flickr.PhotosGetInfo(boek.Afbeelding).MediumUrl;
                    }
                    catch (FlickrNet.Exceptions.PhotoNotFoundException ex)
                    {
                        id.PhotoUrl = null;
                    } catch (System.Net.WebException ex){
                        id.PhotoUrl = null;
                    }
                hipm.FotoIds.Add(id);
            }

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
                if (poi.Afbeelding != null && flickr != null)
                    try
                    {
                        pm.Afbeelding = flickr.PhotosGetInfo(poi.Afbeelding).MediumUrl;

                    }
                    catch (FlickrNet.Exceptions.PhotoNotFoundException ex)
                    {
                        pm.Afbeelding = null;
                    }
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