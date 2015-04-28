using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KaraageCounter.Models;

namespace KaraageCounter.Controllers
{
    public class TurningPointNumbersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TurningPointNumbers
        public ActionResult Index()
        {
            return View(db.TurningPointNumbers.ToList().OrderByDescending(x => x.GetTime));
        }

        // GET: TurningPointNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurningPointNumber turningPointNumber = db.TurningPointNumbers.Find(id);
            if (turningPointNumber == null)
            {
                return HttpNotFound();
            }
            return View(turningPointNumber);
        }

        // GET: TurningPointNumbers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TurningPointNumbers/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Number,GetTime")] TurningPointNumber turningPointNumber)
        {
            if (ModelState.IsValid)
            {
                db.TurningPointNumbers.Add(turningPointNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turningPointNumber);
        }

        // GET: TurningPointNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurningPointNumber turningPointNumber = db.TurningPointNumbers.Find(id);
            if (turningPointNumber == null)
            {
                return HttpNotFound();
            }
            return View(turningPointNumber);
        }

        // POST: TurningPointNumbers/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Number,GetTime")] TurningPointNumber turningPointNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turningPointNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turningPointNumber);
        }

        // GET: TurningPointNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurningPointNumber turningPointNumber = db.TurningPointNumbers.Find(id);
            if (turningPointNumber == null)
            {
                return HttpNotFound();
            }
            return View(turningPointNumber);
        }

        // POST: TurningPointNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TurningPointNumber turningPointNumber = db.TurningPointNumbers.Find(id);
            db.TurningPointNumbers.Remove(turningPointNumber);
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
