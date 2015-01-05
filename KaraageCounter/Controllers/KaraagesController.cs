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

namespace KaraageCounter.Controllers
{
    public class KaraagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Karaages
        public ActionResult Index()
        {
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

            string url = "http://express.heartrails.com/api/json?method=getPrefectures";

            using (var res = WebRequest.Create(url).GetResponse())
            {
                using (var resStream = res.GetResponseStream())
                {
                    var serializer = new DataContractJsonSerializer(typeof(Prefectures));
                    var prefectures = serializer.ReadObject(resStream) as Prefectures;
                    ViewBag.Prefecture = prefectures.response.prefecture;
                }
            }


            ViewBag.Count = 0;
            return View();
        }

        // POST: Karaages/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KaraageID,UserID,CreatedAt")] Karaage karaage)
        {
            if (ModelState.IsValid)
            {

//                string ipa = "";
//                var iphEntry = Dns.GetHostEntry(Dns.GetHostName());
//                foreach (var ipAddr in iphEntry.AddressList)
//                {
//                    if (ipAddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
//                    {
//                        ipa = ipAddr.ToString();
//                        break;
//                    }
//                }
//                if (!string.IsNullOrEmpty(ipa))
//                {
//                    var client = new WebServiceClient(42, "license_key");
//                    var response = client.City(ipa);
//                    Console.WriteLine(response.Country.IsoCode);        // 'US'
//                    Console.WriteLine(response.Country.Name);           // 'United States'
//                    Console.WriteLine(response.MostSpecificSubdivision.Name);    // 'Minnesota'
//                    Console.WriteLine(response.MostSpecificSubdivision.IsoCode); // 'MN'
//                    Console.WriteLine(response.City.Name); // 'Minneapolis'
//
//                }


                karaage.CreatedAt = DateTime.Now;
                db.Karaages.Add(karaage);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.Count = db.Karaages.Count(x => x.UserID == karaage.UserID);

            return View();
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
