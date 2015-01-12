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
using KaraageCounter.Models;
using MaxMind.GeoIP2;
using Microsoft.AspNet.Identity.Owin;

namespace KaraageCounter.Controllers
{
    public class KaraagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            string url = "http://www.telize.com/geoip/" + Request.UserHostAddress;

            ViewBag.IpAddress = Request.UserHostAddress;

            WebClient wc = new WebClient();
            string html = wc.DownloadString(url);

            if (html.Contains("\"city\""))
            {
                var city = html.Substring(html.IndexOf("\"city\"") + "\"city\"".Length + 2);
                ViewBag.City = city.Substring(0, city.IndexOf(",") - 1);
            }
            if (html.Contains("\"region\""))
            {
                var region = html.Substring(html.IndexOf("\"region\"") + "\"region\"".Length + 2);
                ViewBag.Region = region.Substring(0, region.IndexOf(",") - 1);
            }
            if (html.Contains("\"country\""))
            {
                var country = html.Substring(html.IndexOf("\"country\"") + "\"country\"".Length + 2);
                ViewBag.Country = country.Substring(0, country.IndexOf(",") - 1);
            }


            //using Microsoft.AspNet.Identity

            Karaage karaage = new Karaage();
            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            if (manager.AuthenticationManager.User.Identity.IsAuthenticated)
            {
                karaage.UserName = manager.AuthenticationManager.User.Identity.Name;
            }

            //都道府県

//            string url = "http://express.heartrails.com/api/json?method=getPrefectures";
//            using (var res = WebRequest.Create(url).GetResponse())
//            {
//                using (var resStream = res.GetResponseStream())
//                {
//                    var serializer = new DataContractJsonSerializer(typeof(Prefectures));
//                    var prefectures = serializer.ReadObject(resStream) as Prefectures;
//                    ViewBag.Prefecture = prefectures.response.prefecture;
//                }
//            }

            ViewBag.Count = 0;

            return View(karaage);
        }

        // POST: Karaages/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KaraageID,UserName,CreatedAt")] Karaage karaage)
        {
            if (ModelState.IsValid)
            {
                karaage.CreatedAt = DateTime.Now;
                db.Karaages.Add(karaage);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.Count = db.Karaages.Count(x => x.UserName == karaage.UserName);
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
