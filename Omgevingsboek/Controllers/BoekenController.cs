using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using BusinessLogic.Services;
using FlickrNet;
using System.Configuration;
using Omgevingsboek.Config;

namespace Omgevingsboek.Controllers
{
    public class BoekenController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IBoekService bs;
        private Flickr flickr;



        public BoekenController(IBoekService bs)
        {
            this.bs = bs;
            flickr = MvcApplication.flickr;
            if (flickr == null) flickr = FlickrApiManager.GetInstance();
        }
        // GET: Boeken
        
        

        // GET: Boeken/Create
        

        // POST: Boeken/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        //TODO: Verwerken met repos
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Boek boek, HttpPostedFileBase AfbeeldingFile)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index","Home");
            
            String fotoId;
            PhotoInfo fotoInfo;
            if (!ModelState.IsValid) return RedirectToAction("Index");
            
            
            //TODO: geolocatie toevoegen en wanneer word uitgelezen checken of het klopt.
            Boek NieuwBoek = new Boek()
            {
                Naam = boek.Naam,
                EigenaarId = bs.GetUser(User.Identity.Name).Id,
                
            };

            if (AfbeeldingFile != null)
            {
                try
                {
                    fotoId = flickr.UploadPicture(AfbeeldingFile.InputStream, boek.Naam, boek.Naam, "", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden);
                    flickr.PhotosetsAddPhoto(ConfigurationManager.AppSettings.Get("FlickrBoekCoverId"), fotoId);
                    fotoInfo = flickr.PhotosGetInfo(fotoId);
                    NieuwBoek.Afbeelding = fotoInfo.MediumUrl;

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }
            NieuwBoek = bs.InsertBoek(NieuwBoek);
            if (NieuwBoek.Id > 0)
            {
                //feedback?
            }

            return View(boek);
        }
        
    }
}
