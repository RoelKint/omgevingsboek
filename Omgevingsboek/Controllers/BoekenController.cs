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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }





        [HttpPost]
        [Authorize]
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

            
            NieuwBoek = bs.InsertBoek(NieuwBoek);
            if (NieuwBoek.Id > 0)
            {
                if (AfbeeldingFile != null)
                {
                    if (AfbeeldingFile.ContentLength > 10000000) return RedirectToAction("Index", "Home");

                    try
                    {
                        flickr.UploadPictureAsync(AfbeeldingFile.InputStream, boek.Naam, boek.Naam, "", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden, (res) =>
                        {
                            if (!res.HasError)
                            {
                                flickr.PhotosetsAddPhoto(ConfigurationManager.AppSettings.Get("FlickrBoekCoverId"), res.Result);
                                fotoInfo = flickr.PhotosGetInfo(res.Result);
                                bs.UpdateBoekFoto(NieuwBoek.Id, fotoInfo.MediumUrl);
                            }
                        });
                        

                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return RedirectToAction("Index","Home");
        }
        
    }
}
