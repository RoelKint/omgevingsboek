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
using System.Threading;
using System.Threading.Tasks;
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
        public HomeController()
        {

        }
        #region Activiteiten

        [Authorize]
        public ActionResult Activiteit(int? Id)
        {
            if (!Id.HasValue) return RedirectToAction("Index");
            Activiteit activiteit = bs.GetActiviteitById((int)Id);
            if (activiteit == null) return RedirectToAction("Index");
            if (!bs.IsActivityAccessibleByUser((int)Id, User.Identity.Name)) return RedirectToAction("Index");
            return View(activiteit);

        }

        [Authorize]
        public ActionResult GetActiviteit(int? Id)
        {
            if (!Id.HasValue) return RedirectToAction("Index");

            Activiteit a = bs.GetActiviteitById((int)Id);
            if (a == null) return HttpNotFound("Onbestaande Activiteit");
            if (a.Eigenaar.UserName != User.Identity.Name) return HttpNotFound("Geen toegang tot deze activiteit");
            return Json(JsonConvert.SerializeObject(a), JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddActivity(Activiteit activiteit, string TagsString, string BenodigdhedenString, HttpPostedFileBase AfbeeldingFile, string Prijs, int? BoekId, List<HttpPostedFileBase> images, List<string> bestaandefotos, string videos)
        {

            //TODO: lijst van de geselecteerde afbeeldingen?


            ModelState.Remove("Prijs");

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

            if (activiteit.Id == 0)
            {

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

                Activiteit p = bs.InsertActiviteit(NieuweActiviteit);
                if (p.Id > 0)
                {
                    if (AfbeeldingFile != null)
                    {
                        if (AfbeeldingFile.ContentLength > 10000000) return RedirectToAction("Boek", new { id = (int)BoekId });
                        try
                        {
                            flickr.UploadPictureAsync(AfbeeldingFile.InputStream, activiteit.Naam, activiteit.Naam, "", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden, (res) =>
                            {
                                
                                if (!res.HasError)
                                {
                                    flickr.PhotosetsAddPhoto(ConfigurationManager.AppSettings.Get("FlickrActiviteitenAlbumId"), res.Result);
                                    PhotoInfo fotoInfo = flickr.PhotosGetInfo(res.Result);
                                    bs.UpdateActiviteitFoto(p.Id, fotoInfo.MediumUrl);
                                }
                            });


                        }
                        catch (Exception ex)
                        {
                            //return RedirectToAction("Index");
                        }
                    }
                    if (images != null)
                    {
                        try
                        {
                            foreach (HttpPostedFileBase image in images)
                            {
                                if (image.ContentLength > 10000000) continue;
                                //nog fouten met meerdere afbeeldingen
                                flickr.UploadPictureAsync(image.InputStream, activiteit.Naam, activiteit.Naam, "", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden, (res) =>
                                {
                                    if (!res.HasError)
                                    {
                                        PhotoInfo info = flickr.PhotosGetInfo(res.Result);
                                        bs.AddFotoToActiviteit(p.Id, info.LargeUrl);
                                    }
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            //return RedirectToAction("Index");
                        }
                    }
                }
            }
            else
            {
                Activiteit origineeleActiviteit = bs.GetActiviteitById(activiteit.Id);
                if (origineeleActiviteit == null) return RedirectToAction("Boek", new { id = (int)BoekId });
                if (origineeleActiviteit.Eigenaar.UserName != User.Identity.Name) return RedirectToAction("Boek", new { id = (int)BoekId });

                for (int i = 0; i < bestaandefotos.Count; i++)
                {
                    bestaandefotos[i] = bestaandefotos[i].Trim();
                }

                foreach (Foto f in origineeleActiviteit.Fotos)
                {
                    if (!bestaandefotos.Contains(f.FotoUrl))
                        origineeleActiviteit.Fotos.Remove(f);
                }

                Activiteit NieuweActiviteit = new Activiteit()
                {
                    Id = activiteit.Id,
                    Naam = activiteit.Naam,
                    MaxLeeftijd = activiteit.MaxLeeftijd,
                    MinLeeftijd = activiteit.MinLeeftijd,
                    Prijs = activiteit.Prijs,
                    DitactischeToelichting = activiteit.DitactischeToelichting,
                    MinDuur = activiteit.MinDuur,
                    MaxDuur = activiteit.MaxDuur,
                    PoiId = activiteit.PoiId,
                    Uitleg = activiteit.Uitleg,
                    Benodigdheden = benodigdhedenList,
                    Tags = tagList,
                    AfbeeldingNaam = activiteit.AfbeeldingNaam,
                    Fotos = origineeleActiviteit.Fotos
                };

                bs.UpdateActiviteit(NieuweActiviteit);

                if (AfbeeldingFile != null)
                {
                    if (AfbeeldingFile.ContentLength > 10000000) return RedirectToAction("Boek", new { id = (int)BoekId });
                    try
                    {
                        flickr.UploadPictureAsync(AfbeeldingFile.InputStream, activiteit.Naam, activiteit.Naam, "", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden, (res) =>
                        {
                            if (!res.HasError)
                            {
                                flickr.PhotosetsAddPhoto(ConfigurationManager.AppSettings.Get("FlickrActiviteitenAlbumId"), res.Result);
                                PhotoInfo fotoInfo = flickr.PhotosGetInfo(res.Result);
                                bs.UpdateActiviteitFoto(NieuweActiviteit.Id, fotoInfo.MediumUrl);
                            }
                        });


                    }
                    catch (Exception ex)
                    {
                        //return RedirectToAction("Index");
                    }
                }
                if (images != null)
                {
                    try
                    {
                        foreach (HttpPostedFileBase image in images)
                        {
                            if (image.ContentLength > 10000000) return RedirectToAction("Boek", new { id = (int)BoekId });
                            //TODO: Dit hier fixen
                            flickr.UploadPictureAsync(image.InputStream, activiteit.Naam, activiteit.Naam, "", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden, (res) =>
                            {
                                if (!res.HasError)
                                {
                                    PhotoInfo info = flickr.PhotosGetInfo(res.Result);
                                    bs.AddFotoToActiviteit(NieuweActiviteit.Id, info.LargeUrl);
                                }

                            });
                            Thread.Sleep(5);
                        }
                    }
                    catch (Exception ex)
                    {
                        //return RedirectToAction("Index");
                    }
                }

            }
            return RedirectToAction("Boek", new { id = (int)BoekId });

        }
        

        #endregion

        #region Boeken
        [Authorize]
        public ActionResult Boek(int? Id)
        {

            //activities zitten er in
            if (!Id.HasValue) return RedirectToAction("Index");
            Boek boek = bs.GetBoekByID((int)Id);
            if (boek == null) return RedirectToAction("Index");
            if (!bs.IsBoekAccessibleByUser((int)Id, User.Identity.Name)) return RedirectToAction("Index");
            boek.Routes = bs.getRoutesByBoek((int)Id);
            boek.Activiteiten = bs.GetSharedActivitiesByBookId(boek.Id,User.Identity.Name);
            Session.Remove("stap3");
            Session["stap2"] = boek.Naam;
            Session["url2"] = "../home/Boek?id=" + boek.Id;
            ViewBag.stap1 = Session["stap1"];
            ViewBag.url1 = Session["url1"];
            ViewBag.stap2 = Session["stap2"];
            ViewBag.url2 = Session["url2"];

            return View(boek);
        }



        public void SaveBoekenSort(string volgorde, bool? IsGedeeldLijst)
        {
            if (!IsGedeeldLijst.HasValue) return;
            List<BoekOrder> resList = new List<BoekOrder>();

            if (volgorde == null || volgorde == "") return;
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
        #endregion

        #region Gebruiker
        [Authorize]
        public ActionResult Gebruiker(String gebruikerId, int? vanafActiviteit, int? vanafBoek)
        {


            if (!vanafActiviteit.HasValue) vanafActiviteit = 0;
            if (!vanafBoek.HasValue) vanafBoek = 0;

            UserActivities ua = new UserActivities();
            ApplicationUser user = bs.GetUser(gebruikerId);
            ua.Activiteiten = bs.getActiviteitenUserByUser50from((int)vanafActiviteit, user.UserName, User.Identity.Name);
            ua.Boeken = bs.getBoekUserByUser50from((int)vanafBoek, user.UserName, User.Identity.Name);
            ua.User = user;


            Session["stap2"] = user.Naam;
            Session["url2"] = "../home/helpdesk";
            ViewBag.stap1 = Session["stap1"];
            ViewBag.stap2 = Session["stap2"];
            return View(ua);
        }
        
        #endregion


        #region poi
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

        [HttpPost]
        public ActionResult AddTagToPoi(int? PoiId, string tag)
        {
            if (!PoiId.HasValue) return null;
            if (bs.GetPoiById((int)PoiId) == null) return null;

            Models.OmgevingsBoek_Models.Tag t = bs.InsertTag(tag);
            if (bs.getPoiTag(t.ID, (int)PoiId, bs.GetUser(User.Identity.Name).Id) != null) return null;
            bs.AddTagToPoi((int)PoiId, t.ID, User.Identity.Name);

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

            Session.Remove("stap3");
            Session["stap2"] = poi.Naam;
            Session["url2"] = "../home/Poi?id=" + poi.ID;
            ViewBag.stap1 = Session["stap1"];
            ViewBag.url1 = Session["url1"];
            ViewBag.stap2 = Session["stap2"];
            ViewBag.url2 = Session["url2"];
            return View(pm);
        }


        [Authorize]
        public ActionResult AddPoi(Poi poi, HttpPostedFileBase AfbeeldingFile, string TagsString)
        {
            PhotoInfo fotoInfo;
            ModelState.Remove("Prijs");

            if (!ModelState.IsValid) return RedirectToAction("Index");

            String[] tags = TagsString.Split(',');
            List<Models.OmgevingsBoek_Models.PoiTags> tagList = new List<Models.OmgevingsBoek_Models.PoiTags>();

            foreach (string t in tags)
            {
                if (t == "") continue;
                tagList.Add(new PoiTags()
                {
                    EigenaarId = bs.GetUser(User.Identity.Name).Id,
                    TagId = bs.InsertTag(t).ID
                });
            }
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
                Tags = tagList,
                Latitude = poi.Latitude,
                Longitude = poi.Longitude
            };



            Poi p = bs.InsertPoi(NieuwePoi);
            if (p.ID > 0)
            {
                if (AfbeeldingFile != null)
                {
                    try
                    {
                        //TODO: GROTE AFBEELDING FOUT HIER
                        flickr.UploadPictureAsync(AfbeeldingFile.InputStream, poi.Naam, poi.Naam, "", "", false, false, false, ContentType.Photo, SafetyLevel.Safe, HiddenFromSearch.Hidden, (res) =>
                        {
                            if (!res.HasError)
                            {
                                flickr.PhotosetsAddPhoto(ConfigurationManager.AppSettings.Get("FlickrPoiAlbumId"), res.Result);
                                fotoInfo = flickr.PhotosGetInfo(res.Result);
                                bs.UpdatePoiFoto(p.ID, fotoInfo.MediumUrl);
                            }
                        });


                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return RedirectToAction("Index");
        }


        #endregion


        #region routes


        //VERANDERD
        [Authorize]
        public ActionResult GetRouteById(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");
            Route res = bs.getRouteById((int)id);
            if (res == null) return RedirectToAction("Index");
            if (!res.DeelLijst.Any(u => u.UserName == User.Identity.Name)) return RedirectToAction("Index");

            return View(res);

        }
        [Authorize]
        [HttpPost]
        public ActionResult AddRoute2(string routeNaam, string activiteitenIds, int? boekId)
        {
            Route nieuweRoute = new Route();
            nieuweRoute.EigenaarID = bs.GetUser(User.Identity.Name).Id;
            //DIT NOG VERANDEREN
            nieuweRoute.Boeken = new List<Models.OmgevingsBoek_Models.Boek>();
            nieuweRoute.Boeken.Add(bs.GetBoekByID(boekId));
            nieuweRoute.Naam = routeNaam;
            string[] idsSplit = activiteitenIds.Split(',');
            nieuweRoute.RouteLijst = new List<RouteListItem>();
            
            foreach(string a in idsSplit)
            {
                int res;
                if(!int.TryParse(a,out res)) continue;

                Activiteit ac = bs.GetActiviteitById(res);
                if (ac == null) continue;
                if (!bs.IsActivityAccessibleByUser(ac.Id, User.Identity.Name)) continue;
                nieuweRoute.RouteLijst.Add(new RouteListItem()
                {
                    Activiteit = ac
                });
            }
            bs.InsertRoute(nieuweRoute);

            return RedirectToAction("index");

        }


       

        #endregion

        #region basis
        [Authorize]
        public ActionResult Index()
        {
            Session["stap1"] = "Overzicht";
            Session["url1"] = "../Home/";
            Session.Remove("stap2");
            Session.Remove("stap3");
            HomeIndexPM hipm = new HomeIndexPM();
            List<BoekOrder> boEigen = bs.GetBoekOrderLijst(User.Identity.Name, false);
            List<BoekOrder> boGedeeld = bs.GetBoekOrderLijst(User.Identity.Name, true);

            hipm.BoekenEigenaar = new List<Models.OmgevingsBoek_Models.Boek>();
            hipm.BoekenGedeeld = new List<Models.OmgevingsBoek_Models.Boek>();


            foreach (BoekOrder b in boEigen)
            {
                Boek boek = bs.GetBoekByID(b.BoekId);
                if (boek != null)
                    hipm.BoekenEigenaar.Add(boek);
            }

            foreach (BoekOrder b in boGedeeld)
            {
                Boek boek = bs.GetBoekByID(b.BoekId);
                if (boek != null)
                    hipm.BoekenGedeeld.Add(boek);
            }

            ViewBag.UserImageUrl = "";
            ViewBag.stap1 = Session["stap1"];
            ViewBag.url1 = Session["url1"];
            return View(hipm);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Neem hier contact met ons op";

            Session.Remove("stap3");
            Session["stap2"] = "contact";
            Session["url2"] = "../home/Contact";
            ViewBag.stap1 = Session["stap1"];
            ViewBag.url1 = Session["url1"];
            ViewBag.stap2 = Session["stap2"];
            ViewBag.url2 = Session["url2"];
            return View();
        }

        [Authorize]
        public ActionResult HelpDesk()
        {

            Session.Remove("stap3");
            Session["stap2"] = "Helpdesk";
            Session["url2"] = "../home/Helpdesk";
            ViewBag.stap1 = Session["stap1"];
            ViewBag.url1 = Session["url1"];
            ViewBag.stap2 = Session["stap2"];
            ViewBag.url2 = Session["url2"];
            return View();
        }


        #endregion

     
        [Authorize]
        public ActionResult ShareList(int? Id, string type)
        {
            List<ShareListPM> res = new List<ShareListPM>();

            if (!Id.HasValue) return null;
            if (type == null || type == "") return null;
            if(type.ToLower() == "activiteit"){
                Activiteit a = bs.GetActiviteitById((int) Id);
                if(a == null) return null;
                if (a.Eigenaar.UserName != User.Identity.Name) return null;
                List<ApplicationUser> userLijst = bs.GetUsers();
                foreach (ApplicationUser user in userLijst)
                {
                    if (user.UserName == User.Identity.Name) continue;
                    ShareListPM r = new ShareListPM(){
                        Username = user.UserName,
                        Naam = user.Voornaam + " "+ user.Naam
                    };
                    if (a.DeelLijst.Any(w => w.UserName == user.UserName)) r.IsGedeeld = true;
                    else r.IsGedeeld = false;
                    res.Add(r);
                }

            }else if(type.ToLower() == "boek"){
                Boek b = bs.GetBoekByID((int) Id);
                if (b == null) return null;
                if (b.Eigenaar.UserName != User.Identity.Name) return null;
                List<ApplicationUser> userLijst = bs.GetUsers();
                foreach (ApplicationUser user in userLijst)
                {
                    if (user.UserName == User.Identity.Name) continue;
                    ShareListPM r = new ShareListPM()
                    {
                        Username = user.UserName,
                        Naam = user.Voornaam + " " + user.Naam
                    };
                    if (b.DeelLijst.Any(w => w.UserName == user.UserName)) r.IsGedeeld = true;
                    else r.IsGedeeld = false;
                    res.Add(r);
                }

            }else return null;

            return Json(JsonConvert.SerializeObject(res), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public void EditShare(string Username, int Id, string Type, bool IsGedeeld)
        {
            if (!ModelState.IsValid) return;
            ApplicationUser user = bs.GetUser(Username);
            if (user == null) return;
            if (Type.ToLower() == "activiteit")
            {
                Activiteit a = bs.GetActiviteitById((int)Id);
                if (a == null) return;
                if (a.Eigenaar.UserName != User.Identity.Name) return;
                bs.addUserToActiviteitShareList(a.Id, user.UserName,IsGedeeld);

            }
            else if (Type.ToLower() == "boek")
            {
                Boek b = bs.GetBoekByID((int)Id);
                if (b == null) return;
                if (b.Eigenaar.UserName != User.Identity.Name) return;
                bs.addUserToBoekShareList(b.Id, user.UserName, IsGedeeld);
            }
            else return;

        }

      
      
        [ChildActionOnly]
        public ActionResult PoiPartial()
        {
            List<PoiPM> poipms = new List<PoiPM>();
            List<Poi> pois = bs.GetPoiList();
            foreach (Poi poi in pois)
            {
                PoiPM pm = new PoiPM()
                {
                    poi = poi
                };
                pm.Activiteiten = bs.getActiviteitenByPoiByUser50from(0, User.Identity.Name, poi.ID);
                poipms.Add(pm);

            }
            return PartialView("_PoiPartial", JsonConvert.SerializeObject(poipms));
        }

        [ChildActionOnly]
        public ActionResult ActiviteitPartial()
        {
            List<ActiviteitPM> activiteitenpms = new List<ActiviteitPM>();
            List<Activiteit> activiteiten = bs.GetSharedActivitiesByUsername(User.Identity.Name);

            foreach (Activiteit act in activiteiten)
            {
                ActiviteitPM pm = new ActiviteitPM();

                pm.act = act;

                activiteitenpms.Add(pm);
            }

            return PartialView("_ActiviteitPartial", JsonConvert.SerializeObject(activiteitenpms));
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
        
    }
}