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
        
        [Authorize]
        public ActionResult Index()
        {
            HomeIndexPM hipm = new HomeIndexPM();
            List<BoekOrder> boEigen = bs.GetBoekOrderLijst(User.Identity.Name, false);
            List<BoekOrder> boGedeeld = bs.GetBoekOrderLijst(User.Identity.Name, true);

            hipm.BoekenEigenaar = new List<Models.OmgevingsBoek_Models.Boek>();
            hipm.BoekenGedeeld = new List<Models.OmgevingsBoek_Models.Boek>();


            foreach (BoekOrder b in boEigen)
            {
                hipm.BoekenEigenaar.Add(bs.GetBoekByID(b.BoekId));
            }

            foreach (BoekOrder b in boGedeeld)
            {
                hipm.BoekenGedeeld.Add(bs.GetBoekByID(b.BoekId));
            }

            ViewBag.UserImageUrl = "";

            return View(hipm);
        }
        [Authorize]
        public ActionResult Boek(int? Id)
        {
            //activities zitten er in
            if (!Id.HasValue) return RedirectToAction("Index");
            Boek boek = bs.GetBoekByID((int)Id);
            if (boek == null) return RedirectToAction("Index");
            if (!bs.IsBoekAccessibleByUser((int) Id,User.Identity.Name)) return RedirectToAction("Index");

            return View(boek);
        }

        [Authorize]
        public String RemoveTag(int? TagId, int? PoiId)
        {
            if (!TagId.HasValue || !PoiId.HasValue) return "NOK";
            ApplicationUser user = bs.GetUser(User.Identity.Name);
            PoiTags tag = bs.getPoiTag((int)TagId, (int)PoiId, user.Id);
            
            if (tag == null) return "NOK";
            bs.DeletePoiTag(tag);

            return "OK";
        }

        public void SaveBoekenSort(string volgorde, bool? IsGedeeldLijst)
        {
            if (!IsGedeeldLijst.HasValue) return;
            List<BoekOrder> resList = new List<BoekOrder>();

            if(volgorde == null || volgorde == "") return;
            ApplicationUser user = bs.GetUser(User.Identity.Name);
            volgorde = volgorde.Replace(",toevoegenBoek", "");
            string[] splitted = volgorde.Split(',');
            List<int> ids = new List<int>();
            foreach (string str in splitted)
            {
                bool check;
                int res;
                check = int.TryParse(str, out res);
                if (!check) return;
                if (!bs.IsBoekAccessibleByUser(res, User.Identity.Name)) return;
                if (ids.Contains(res)) return;
                ids.Add(res);

            }
            if (bs.GetBoekOrderLijst(User.Identity.Name, (bool)IsGedeeldLijst).Count() != ids.Count()) return;
            int count = 0;
            foreach (int id in ids)
            {
                resList.Add(new BoekOrder()
                {
                    BoekId = id,
                    EigenaarId = user.Id,
                    Index = count,
                    IsSharedLijst = (bool)IsGedeeldLijst
                });
                count++;
            }
            bs.UpdateLijst(resList);
            
        }


        [Authorize]
        public ActionResult Activiteit(int? Id)
        {
            if (!Id.HasValue) return RedirectToAction("Index");
            Activiteit activiteit = bs.GetActiviteitById((int)Id);
            if (activiteit == null) return RedirectToAction("Index");
            if (!bs.IsActivityAccessibleByUser((int)Id, User.Identity.Name)) return RedirectToAction("Index");
            
            return View(activiteit);
        }
        [HttpPost]
        public ActionResult AddTagToPoi(int? PoiId, string tag)
        {
            if(!PoiId.HasValue) return null; 
            if (bs.GetPoiById((int)PoiId) == null) return null;
           
            Models.OmgevingsBoek_Models.Tag t = bs.InsertTag(tag);
            if (bs.getPoiTag(t.ID, (int)PoiId, bs.GetUser(User.Identity.Name).Id) != null) return null;
            bs.AddTagToPoi((int)PoiId, t.ID,User.Identity.Name);
            
            return null;
        }

        public ActionResult GetTagsByPoi(int? PoiId)
        {
            if (!PoiId.HasValue) return null;
            if (bs.GetPoiById((int)PoiId) == null) return null;
            List<Models.OmgevingsBoek_Models.PoiTags> tags = bs.getTagsByPoi((int)PoiId);
            //List<SimpleTag> stl = new List<SimpleTag>();
            //List<String> tagList = new List<String>();
            
            return Json(JsonConvert.SerializeObject(tags), JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult Poi(int? Id)
        {
            if (!Id.HasValue) return RedirectToAction("Index");
            Poi poi = bs.GetPoiById((int)Id);
            if (poi == null) return RedirectToAction("Index");
            List<Poi> pois = bs.GetPoiList();
            
            PoiPM pm = new PoiPM()
            {
                poi = poi
            };
            pm.Activiteiten = bs.getActiviteitenByPoiByUser50from(0, User.Identity.Name, poi.ID);
            ViewBag.gebruikerId = bs.GetUser(User.Identity.Name).Id;
            return View(pm);
        }
        [Authorize]
        
        public ActionResult AddPoi(Poi poi, HttpPostedFileBase AfbeeldingFile,string TagsString)
        {
            String fotoId;
            PhotoInfo fotoInfo;
            if (!ModelState.IsValid) return RedirectToAction("Index");
            
            String[] tags = TagsString.Split(',');
            List<Models.OmgevingsBoek_Models.PoiTags> tagList = new List<Models.OmgevingsBoek_Models.PoiTags>();

            foreach (string t in tags)
            {
                if (t == "") continue;
                tagList.Add(new PoiTags(){
                    EigenaarId = bs.GetUser(User.Identity.Name).Id,
                    TagId = bs.InsertTag(t).ID
                });
            }
            //TODO: geolocatie toevoegen en wanneer word uitgelezen checken of het klopt.
            Poi NieuwePoi = new Poi()
            {
                Naam = poi.Naam,
                EigenaarId = bs.GetUser(User.Identity.Name).Id,
                Email = poi.Email,
                Gemeente = poi.Gemeente,
                MaxLeeftijd = poi.MaxLeeftijd,
                MinLeeftijd = poi.MinLeeftijd,
                Nummer = poi.Nummer,
                Postcode = poi.Postcode,
                Prijs = poi.Prijs,
                Straat = poi.Straat,
                Telefoon = poi.Telefoon,
                Tags = tagList
            };

            if (AfbeeldingFile != null)
            {
                try
                {
                    fotoId = flickr.UploadPicture(AfbeeldingFile.InputStream, poi.Naam, poi.Naam, "", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden);
                    flickr.PhotosetsAddPhoto(ConfigurationManager.AppSettings.Get("FlickrPoiAlbumId"), fotoId);
                    fotoInfo = flickr.PhotosGetInfo(fotoId);
                    NieuwePoi.Afbeelding = fotoInfo.MediumUrl;

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }

            Poi p = bs.InsertPoi(NieuwePoi);
            if (p.ID > 0)
            {
                //Feedback???
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Neem hier contact met ons op";

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

        [Authorize]        
        [HttpPost]
        public void UpdateAfbeelding(HttpPostedFileBase Afbeelding)
        {
            string UserName = User.Identity.Name;
            ApplicationUser appuser = bs.GetUser(UserName);
            string fotoId = flickr.UploadPicture(Afbeelding.InputStream, UserName, UserName, "", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden);
            flickr.PhotosetsAddPhoto(ConfigurationManager.AppSettings.Get("FlickrGebruikersAlbumId"), fotoId);
            PhotoInfo fotoInfo = flickr.PhotosGetInfo(fotoId);
            appuser.Afbeelding = fotoInfo.MediumUrl;
            bs.UpdateUserAfbeelding(appuser);

        }


        public ActionResult GetTags()
        {
            List<Models.OmgevingsBoek_Models.Tag> tags = bs.GetTagList();
            List<SimpleTag> stl = new List<SimpleTag>();
            List<String> tagList = new List<String>();
            foreach (Models.OmgevingsBoek_Models.Tag tag in tags)
            {
                stl.Add(new SimpleTag()
                {
                    Id = tag.ID,
                    Naam = tag.Naam
                });

                tagList.Add(tag.Naam);

            }
            return Json(JsonConvert.SerializeObject(tagList), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBenodigdheden()
        {
            List<string> res = new List<string>();
            foreach (Benodigdheid b in bs.GetBenodigdhedenList())
            {
                res.Add(b.Naam);
            }
            return Json(JsonConvert.SerializeObject(res), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddActivity(Activiteit activiteit, string TagsString, string BenodigdhedenString, HttpPostedFileBase AfbeeldingFile, string Prijs, int? BoekId)
        {
            String fotoId;
            
            PhotoInfo fotoInfo;

            if (!BoekId.HasValue) return RedirectToAction("Index");
            if (bs.GetBoekByID((int)BoekId) == null) return RedirectToAction("index");
            if (!ModelState.IsValid) return RedirectToAction("Boek", new { id = (int)BoekId });
            if (bs.GetPoiById(activiteit.PoiId) == null) return RedirectToAction("Boek", new { id = (int)BoekId });


            String[] tags = TagsString.Split(',');
            List<Models.OmgevingsBoek_Models.Tag> tagList = new List<Models.OmgevingsBoek_Models.Tag>();

            foreach (string t in tags)
            {
                if (t == "") continue;
                tagList.Add(bs.InsertTag(t));
            }


            String[] benodigdheden = TagsString.Split(',');
            List<Benodigdheid> benodigdhedenList = new List<Benodigdheid>();

            foreach (string b in benodigdheden)
            {
                if (b == "") continue;
                tagList.Add(bs.InsertTag(b));
            }


            Activiteit NieuweActiviteit = new Activiteit()
            {
                Naam = activiteit.Naam,
                EigenaarId = bs.GetUser(User.Identity.Name).Id,
                MaxLeeftijd = activiteit.MaxLeeftijd,
                MinLeeftijd = activiteit.MinLeeftijd,
                Prijs = activiteit.Prijs,
                DitactischeToelichting = activiteit.DitactischeToelichting,
                MinDuur = activiteit.MinDuur,
                MaxDuur = activiteit.MaxDuur,
                PoiId = activiteit.PoiId,
                Uitleg = activiteit.Uitleg,
                
                
                
                Benodigdheden = benodigdhedenList,
                Tags = tagList
            };

            NieuweActiviteit.Boeken = new List<Boek>();
            NieuweActiviteit.Boeken.Add(bs.GetBoekByID((int)BoekId));
            if (AfbeeldingFile != null)
            {
                try
                {
                    fotoId = flickr.UploadPicture(AfbeeldingFile.InputStream, activiteit.Naam, activiteit.Naam, "", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden);
                    flickr.PhotosetsAddPhoto(ConfigurationManager.AppSettings.Get("FlickrPoiAlbumId"), fotoId);
                    fotoInfo = flickr.PhotosGetInfo(fotoId);
                    NieuweActiviteit.AfbeeldingNaam = fotoInfo.MediumUrl;

                }
                catch (Exception ex)
                {
                    //return RedirectToAction("Index");
                }
            }

            Activiteit p = bs.InsertActiviteit(NieuweActiviteit);
            if (p.Id > 0)
            {
                //Feedback???
            }

            return RedirectToAction("Boek", new { id = (int)BoekId });
        }
    }
}