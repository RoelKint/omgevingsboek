using BusinessLogic.Repositories;
using BusinessLogic.Services;
using FlickrNet;
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using Models.PresentationModels;
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
            flickr = FlickrApiManager.GetInstance();

        }

        [Authorize]
        public ActionResult Index()
        {
            //string a = Request.QueryString["oauth_verifier"];
            //OAuthAccessToken b = flickr.OAuthGetAccessToken(flickr.OAuthGetRequestToken("obb"), a);


            HomeIndexPM hipm = new HomeIndexPM();
            hipm.BoekenEigenaar = bs.getBoekenByUser(User.Identity.Name);
            hipm.BoekenGedeeld = bs.getSharedBoeken(User.Identity.Name);
            hipm.FotoIds = new List<FotoId>();
            foreach (Boek boek in hipm.BoekenEigenaar)
            {
                FotoId id = new FotoId()
                {
                    Id = boek.Id
                };
                if (boek.Afbeelding != null && flickr != null)
                    id.PhotoUrl = flickr.PhotosGetInfo(boek.Afbeelding).MediumUrl;
                hipm.FotoIds.Add(id);
            }
            foreach (Boek boek in hipm.BoekenGedeeld )
            {
                FotoId id = new FotoId()
                {
                    Id = boek.Id
                };
                if (boek.Afbeelding != null && flickr != null)
                    id.PhotoUrl = flickr.PhotosGetInfo(boek.Afbeelding).MediumUrl;
                hipm.FotoIds.Add(id);
            }

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

        public ActionResult Test()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Test(HttpPostedFileBase picture)
        {
            String a = flickr.UploadPicture(picture.InputStream, "test", "test", "test", "", true, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden);
            return View();
        }
    }
}