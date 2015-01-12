using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DateTimeExtensions;
using KaraageCounter.Models;
using KaraageCounter.Properties;
using MaxMind.GeoIP2;
using Microsoft.AspNet.Identity.Owin;

namespace KaraageCounter.Controllers
{
    public class KaraagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static int UnKnownCounter = 0;

        private int GetTodayUserCount(string userName, IEnumerable<Karaage> karaages)
        {
            if (userName == Resources.UnknownUserName)
            {
                return UnKnownCounter;
            }
            else
            {
                return karaages
                    .Where(x => x.CreatedAt.IsToday())
                    .Count(x => x.UserName == userName);
            }
        }



        // GET: Karaages
        public ActionResult Index()
        {
            ViewBag.Count = db.Karaages.Count();
            return View(db.Karaages.ToList());
        }


       

        // GET: Karaages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Karaage karaage = db.Karaages.Find(id);
            if (karaage == null)
            {
                return HttpNotFound();
            }
            return View(karaage);
        }

        // GET: Karaages/Create
        public ActionResult Create()
        {
            Karaage karaage = new Karaage();

            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            if (manager.AuthenticationManager.User.Identity.IsAuthenticated)
            {
                karaage.UserName = manager.AuthenticationManager.User.Identity.Name;
            }
            else
            {
                karaage.UserName = Resources.UnknownUserName;
            }
            ViewBag.Count = GetTodayUserCount(karaage.UserName, db.Karaages.ToList());
            return View(karaage);
        }


        // POST: Karaages/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,CreatedAt")] Karaage karaage)
        {
            if (ModelState.IsValid)
            {
                karaage.CreatedAt = DateTime.Now;
                db.Karaages.Add(karaage);
                db.SaveChanges();

//                var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationSignInManager>();
//                if (manager.AuthenticationManager.User.Identity.IsAuthenticated)
//                {
//
//                    ViewBag.Count = db.Karaages.Where(x => x.CreatedAt != null && x.CreatedAt.IsToday()).Count(x => x.UserName == karaage.UserName);
//                }
//                else
//                {
//                    ViewBag.Count = UnKnownCounter++;
//                }
                UnKnownCounter++;
                ViewBag.Count = GetTodayUserCount(karaage.UserName, db.Karaages.ToList());

                //return RedirectToAction("Index");
            }
            return View(karaage);
        }

        // GET: Karaages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Karaage karaage = db.Karaages.Find(id);
            if (karaage == null)
            {
                return HttpNotFound();
            }
            return View(karaage);
        }

        // POST: Karaages/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KaraageID,UserID,CreatedAt")] Karaage karaage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(karaage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(karaage);
        }

        // GET: Karaages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Karaage karaage = db.Karaages.Find(id);
            if (karaage == null)
            {
                return HttpNotFound();
            }
            return View(karaage);
        }

        // POST: Karaages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Karaage karaage = db.Karaages.Find(id);
            db.Karaages.Remove(karaage);
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
