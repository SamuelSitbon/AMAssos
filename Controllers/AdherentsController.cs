﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMAssos;
using AMAssos.Utilities;

namespace AMAssos.Controllers
{
    public class AdherentsController : Controller
    {
        private AMAssosEntities db = new AMAssosEntities();

        // GET: Adherents
        public ActionResult Index()
        {
            var adherents = db.Adherents.Include(a => a.Association);
            return View(adherents.ToList());
        }

        // GET: Adherents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = db.Adherents.Find(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            return View(adherent);
        }

        // GET: Adherents/Create
        public ActionResult Create()
        {
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom");
            return View();
        }

        // POST: Adherents/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAdherent,IdAssociation,Matricule,Nom,Prenom,Email,Telephone,Cotisation,Login,Password,Responsable,Solde")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Adherents.Add(adherent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", adherent.IdAssociation);
            return View(adherent);
        }

        // GET: Adherents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = db.Adherents.Find(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", adherent.IdAssociation);
            return View(adherent);
        }

        // POST: Adherents/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAdherent,IdAssociation,Matricule,Nom,Prenom,Email,Telephone,Cotisation,Login,Password,Responsable,Solde")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adherent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", adherent.IdAssociation);
            return View(adherent);
        }

        // GET: Adherents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = db.Adherents.Find(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            return View(adherent);
        }

        // POST: Adherents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adherent adherent = db.Adherents.Find(id);
            db.Adherents.Remove(adherent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Connexion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Connexion([Bind(Include = "Login,Password")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {

                var adherents = db.Adherents.Where(p => p.Login == adherent.Login && p.Password == adherent.Password).ToList();

                if (adherents != null && adherents.Count() == 1)
                {
                    adherent = adherents.First();

                    HttpContext.Session.Add(Constants.SESSION_ADHERENT, adherent);

                    db.SaveChanges();
                    Session[Constants.SESSION_ADHERENT] = adherent;


                    return RedirectToAction("Details", "Associations", new { id = adherent.IdAssociation });
                }
                else
                {
                    ViewBag.Erreur = "Erreur d'identification";
                }
            }

            return View(adherent);
        }

        public ActionResult Deconnexion()
        {
            if (Session[Constants.SESSION_ADHERENT] != null)
            {
               Session[Constants.SESSION_ADHERENT] = null;
            }

            return RedirectToAction("Connexion", "Adherents");
        }
    }
}
