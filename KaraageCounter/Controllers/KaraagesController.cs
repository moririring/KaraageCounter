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
        private Random RandomNumber;

        public KaraagesController()
        {
            var seed = Environment.TickCount;
            RandomNumber = new Random(seed++);

        }

        private int GetTodayUserCount(string userName, string IpAddress, IEnumerable<Karaage> karaages)
        {
            return karaages
                .Where(x => x.CreatedAt.IsToday())
                .Count(x => x.UserName == userName && x.IpAddress == IpAddress);
        }


        // GET: Karaages
        public ActionResult Index()
        {
            var karaageList = db.Karaages.ToList();

            ViewBag.Count = karaageList.Count();
            ViewBag.TodayCount = karaageList.Where(x => x.CreatedAt.IsToday()).Count();
            ViewBag.YesterdayCount = karaageList.Where(x => x.CreatedAt.IsYesterday()).Count();
            return View(karaageList);
   
        }

        public ActionResult Ranking()
        {
            return View(db.Rankings.ToList());
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
            


            karaage.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
            //RankingUpdate(karaage.UserName);
            //TurningPointNumberUpdate(karaage.UserName);
            ViewBag.Count = GetTodayUserCount(karaage.UserName, karaage.IpAddress, db.Karaages.ToList());
            ViewBag.Click = false;
            return View(karaage);
        }


        public void RankingUpdate(string userName)
        {
            if (userName != Resources.UnknownUserName)
            {
                var hit = db.Rankings.FirstOrDefault(x => x.UserName == userName);
                if (hit != null)
                {
                    hit.KaraageCount++;
                }
                else
                {
                    var ranking = new Ranking()
                    {
                        UserName = userName,
                        KaraageCount = db.Karaages.Count(x => x.UserName == userName),
                    };
                    db.Rankings.Add(ranking);
                }
            }
        }

        public void TurningPointNumberUpdate(string userName, int count)
        {
            var turning = Math.Pow(10, count.ToString().Length - 1);
            if (count % turning == 0)
            {
                var turningPointNumber = new TurningPointNumber()
                {
                    UserName = userName,
                    Number = count,
                    GetTime = DateTime.Now,
                };
                db.TurningPointNumbers.Add(turningPointNumber);
            }
        }

        // POST: Karaages/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,CreatedAt")] Karaage karaage)
        {
            ViewBag.Click = false;
            if (ModelState.IsValid)
            {
                karaage.RandNumber = RandomNumber.Next(1000);
                karaage.CreatedAt = DateTime.Now;
                karaage.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                db.Karaages.Add(karaage);

                //SaveChangesするまで反映されない。先に呼ぶと2回呼ぶ必要がある
                var count = db.Karaages.ToList().Count + 1;
                RankingUpdate(karaage.UserName);
                TurningPointNumberUpdate(karaage.UserName, count);

                db.SaveChanges();


                ViewBag.Count = GetTodayUserCount(karaage.UserName, karaage.IpAddress, db.Karaages.ToList());
                ViewBag.Click = true;
                ViewBag.RandNumber = karaage.RandNumber;
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
