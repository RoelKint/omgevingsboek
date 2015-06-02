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
            Flickr flickr = FlickrApiManager.GetInstance();
            
            PhotosetPhotoCollection photos = flickr.PhotosetsGetPhotos("72157653473597658",PrivacyFilter.CompletelyPrivate);
            PhotoInfo i = flickr.PhotosGetInfo(photos[0].PhotoId);
            
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