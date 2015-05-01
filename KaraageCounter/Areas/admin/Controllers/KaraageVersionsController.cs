using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KaraageCounter.Areas.admin.Models;
using KaraageCounter.Models;

namespace KaraageCounter.Areas.admin.Controllers
{
    public class KaraageVersionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: admin/KaraageVersions
        public async Task<ActionResult> Index()
        {
            return View(await db.KaraageVersions.ToListAsync());
        }

        // GET: admin/KaraageVersions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KaraageVersion karaageVersion = await db.KaraageVersions.FindAsync(id);
            if (karaageVersion == null)
            {
                return HttpNotFound();
            }
            return View(karaageVersion);
        }

        // GET: admin/KaraageVersions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/KaraageVersions/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Released,Version,Content,Date,Url")] KaraageVersion karaageVersion)
        {
            if (ModelState.IsValid)
            {
                db.KaraageVersions.Add(karaageVersion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(karaageVersion);
        }

        // GET: admin/KaraageVersions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KaraageVersion karaageVersion = await db.KaraageVersions.FindAsync(id);
            if (karaageVersion == null)
            {
                return HttpNotFound();
            }
            return View(karaageVersion);
        }

        // POST: admin/KaraageVersions/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Released,Version,Content,Date,Url")] KaraageVersion karaageVersion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(karaageVersion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(karaageVersion);
        }

        // GET: admin/KaraageVersions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KaraageVersion karaageVersion = await db.KaraageVersions.FindAsync(id);
            if (karaageVersion == null)
            {
                return HttpNotFound();
            }
            return View(karaageVersion);
        }

        // POST: admin/KaraageVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            KaraageVersion karaageVersion = await db.KaraageVersions.FindAsync(id);
            db.KaraageVersions.Remove(karaageVersion);
            await db.SaveChangesAsync();
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
