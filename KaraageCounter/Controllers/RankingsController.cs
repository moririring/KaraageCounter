using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KaraageCounter.Models;
using KaraageCounter.Properties;

namespace KaraageCounter.Controllers
{
    public class RankingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rankings
        public ActionResult Index()
        {
            return View(db.Rankings.Where(r => r.UserName != Resources.UnknownUserName).ToList());
        }

        // GET: Rankings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranking ranking = db.Rankings.Find(id);
            if (ranking == null)
            {
                return HttpNotFound();
            }
            return View(ranking);
        }

        // GET: Rankings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rankings/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,IpAddress,SessionId,KaraageCount")] Ranking ranking)
        {
            if (ModelState.IsValid)
            {
                db.Rankings.Add(ranking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ranking);
        }

        // GET: Rankings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranking ranking = db.Rankings.Find(id);
            if (ranking == null)
            {
                return HttpNotFound();
            }
            return View(ranking);
        }

        // POST: Rankings/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,IpAddress,SessionId,KaraageCount")] Ranking ranking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ranking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ranking);
        }

        // GET: Rankings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranking ranking = db.Rankings.Find(id);
            if (ranking == null)
            {
                return HttpNotFound();
            }
            return View(ranking);
        }

        // POST: Rankings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ranking ranking = db.Rankings.Find(id);
            db.Rankings.Remove(ranking);
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
