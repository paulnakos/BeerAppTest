using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeerAppTest.DAL;
using BeerAppTest.Models;

namespace BeerAppTest.Controllers
{
    public class UserController : Controller
    {
        private BeerAppTestContext db = new BeerAppTestContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Location).Include(u => u.UserCredentials);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        private void Locations(object selectedLocation = null)
        {
            var locations = from l in db.Locations
                            orderby l.Country
                            select l;

            ViewBag.LocationID = new SelectList(locations.Distinct(), "locationID", "country", selectedLocation);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            Locations();
            //ViewBag.LocationID = new SelectList(db.Locations.Distinct(), "LocationID", "Country");
            //ViewBag.UserID = new SelectList(db.UserCredentials, "UserID", "UserName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Gender,LastName,FirstName,BirtDate,LocationID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            Locations(user.LocationID);
            //ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Country", user.LocationID);
            //ViewBag.UserID = new SelectList(db.UserCredentials, "UserID", "UserName", user.UserID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Locations(user.LocationID);
            //ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Country", user.LocationID);
            //ViewBag.UserID = new SelectList(db.UserCredentials, "UserID", "UserName", user.UserID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Gender,LastName,FirstName,BirtDate,LocationID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Country", user.LocationID);
            ViewBag.UserID = new SelectList(db.UserCredentials, "UserID", "UserName", user.UserID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
    }
}
