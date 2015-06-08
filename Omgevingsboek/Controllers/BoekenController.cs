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

namespace Omgevingsboek.Controllers
{
    public class BoekenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IBoekService bs;


        public BoekenController(IBoekService bs)
        {
            this.bs = bs;
        }
        // GET: Boeken
        public ActionResult Index()
        {
            var boeken = db.Boeken.Include(b => b.Eigenaar);
            return View(boeken.ToList());
        }

        // GET: Boeken/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boek boek = db.Boeken.Find(id);
            if (boek == null)
            {
                return HttpNotFound();
            }
            return View(boek);
        }

        // GET: Boeken/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boeken/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naam,Afbeelding,EigenaarId,IsDeleted")] Boek boek)
        {
            if (ModelState.IsValid)
            {
                boek.EigenaarId = bs.GetUser(User.Identity.Name).Id;
                db.Boeken.Add(boek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.EigenaarId = new SelectList(db.ApplicationUsers, "Id", "Naam", boek.EigenaarId);
            return View(boek);
        }

        //// GET: Boeken/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Boek boek = db.Boeken.Find(id);
        //    if (boek == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    //ViewBag.EigenaarId = new SelectList(db.ApplicationUsers, "Id", "Naam", boek.EigenaarId);
        //    return View(boek);
        //}

        //// POST: Boeken/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Naam,Afbeelding,EigenaarId,IsDeleted")] Boek boek)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(boek).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EigenaarId = "13d768d6-fb4c-46b9-bb0a-73c8e7014652";
        //    //ViewBag.EigenaarId = new SelectList(db.ApplicationUsers, "Id", "Naam", boek.EigenaarId);
        //    return View(boek);
        //}

        //// GET: Boeken/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Boek boek = db.Boeken.Find(id);
        //    if (boek == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(boek);
        //}

        //// POST: Boeken/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Boek boek = db.Boeken.Find(id);
        //    db.Boeken.Remove(boek);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
