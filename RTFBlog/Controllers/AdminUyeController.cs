using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RTFBLOG.Models;

namespace RTFBLOG.Controllers
{
    public class AdminUyeController : Controller
    {
        private yeniBlogDbEntities db = new yeniBlogDbEntities();

        // GET: AdminUye
        public ActionResult Index()
        {
            var uye = db.Uye.Include(u => u.Yetki);
            return View(uye.ToList());
        }

        // GET: AdminUye/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // GET: AdminUye/Create
        public ActionResult Create()
        {
            ViewBag.YetkiId = new SelectList(db.Yetki, "YetkiId", "Yetki1");
            return View();
        }

        // POST: AdminUye/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Uye uye, HttpPostedFileBase foto)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(foto.InputStream);
                FileInfo fotoInfo = new FileInfo(foto.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoInfo.Extension;
                
               

                img.Resize(150, 150);
                img.Save("~/Uploads/UyeFoto/" + newfoto);
                uye.Foto = "/Uploads/UyeFoto/" + newfoto.ToString();
        
              

                db.Uye.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.YetkiId = new SelectList(db.Yetki, "YetkiId", "Yetki1", uye.YetkiId);
            return View(uye);
        }

        // GET: AdminUye/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            ViewBag.YetkiId = new SelectList(db.Yetki, "YetkiId", "Yetki1", uye.YetkiId);
            return View(uye);
        }

        // POST: AdminUye/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UyeId,KullaniciAdi,Email,Sifre,AdSoyad,YetkiId")] Uye uye, HttpPostedFileBase foto)
        {
            if (ModelState.IsValid)
            {
                if (null!=foto)
                {
                    if (System.IO.File.Exists(Server.MapPath(uye.Foto)))
                    {

                        System.IO.File.Delete(Server.MapPath(uye.Foto));
                    }
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoInfo.Extension;



                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uye.Foto = "/Uploads/UyeFoto/" + newfoto.ToString();
                   
                }

                db.Entry(uye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.YetkiId = new SelectList(db.Yetki, "YetkiId", "Yetki1", uye.YetkiId);
            return View(uye);
        }

        // GET: AdminUye/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: AdminUye/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uye uye = db.Uye.Find(id);
            db.Uye.Remove(uye);
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
