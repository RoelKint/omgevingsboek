using BusinessLogic.Services;
using FlickrNet;
using Omgevingsboek.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Omgevingsboek.Controllers
{
    public class ActiviteitenController : Controller
    {
        private IBoekService bs;
        private Flickr flickr;

        public ActiviteitenController(IBoekService bs)
        {
            this.bs = bs;
            flickr = MvcApplication.flickr;
            if (flickr == null) flickr = FlickrApiManager.GetInstance();
        }

        #region DETAILS
        // GET: Activiteiten/Details/5
        public ActionResult Details(int id)
        {
            return View(bs.GetActiviteitById(id));
        }
        #endregion

        #region EDIT
        public ActionResult Edit(int id)
        {
            return View(new NotImplementedException());
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return View(new NotImplementedException());
                //return RedirectToAction("Index");
            }
            catch
            {
                return View(new NotImplementedException());
            }
        }
        #endregion

        #region DELETE
        public ActionResult Delete(int id)
        {
            return View(bs.GetActiviteitById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                bs.DeleteActiviteitSoft(bs.GetActiviteitById(id));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
