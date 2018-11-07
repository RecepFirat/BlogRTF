using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RTFBLOG.Models;
using PagedList;
using PagedList.Mvc;
namespace RTFBLOG.Controllers
{
    public class AdminMakaleController : Controller
    {
        yeniBlogDbEntities db = new yeniBlogDbEntities();
        // GET: AdminMakale
        public ActionResult Index(int Page=1)
        {
            var makale = db.Makale.OrderByDescending(x=>x.MakaleId).ToPagedList(Page,1);
            return View(makale);
        }

        // GET: AdminMakale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminMakale/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriId", "KategoriAdi");//neyi cekicegim ve bize gözükecek alanı yazıyorum
            return View();
        }

        // POST: AdminMakale/Create
        [HttpPost]
        public ActionResult Create(Makale makale, string Etiketler, HttpPostedFileBase Foto)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    if (null != Foto)
                    {
                        WebImage img = new WebImage(Foto.InputStream);
                        FileInfo fotoInfo = new FileInfo(Foto.FileName);

                        string newfoto = Guid.NewGuid().ToString() + fotoInfo.Extension;

                        img.Resize(750, 300);
                        img.Save("~/Uploads/MakaleFoto/" + newfoto);
                        makale.Foto = "/Uploads/MakaleFoto/" + newfoto.ToString();



                    }
                    if (null != Etiketler)
                    {
                        string[] etiketdizi = Etiketler.Split(',');

                        foreach (var item in etiketdizi)
                        {
                            var Yenietiket = new Etiket { EtiketAdi = item };
                            db.Etiket.Add(Yenietiket);
                            makale.Etiket.Add(Yenietiket);
                        }
                    }
                    makale.UyeId =Convert.ToInt32(Session["uyeid"].ToString());
                    makale.Okunma = 0;
                    makale.Tarih = DateTime.Now;
                    db.Makale.Add(makale);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: AdminMakale/Edit/5
        public ActionResult Edit(int id)
        {

            var makale = db.Makale.Where(x => x.MakaleId == id).SingleOrDefault();
            if (null==makale)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID=new SelectList(db.Kategori, "KategoriId", "KategoriAdi",makale.KategoriId);//ekstradan buraya secilen makalenın ıdsınıde gonderıyoruz
            return View(makale);
        }

        // POST: AdminMakale/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase foto,Makale makale)
        {
            try
            {
                // TODO: Add update logic here
                var Guncellenecekmakale = db.Makale.Where(x => x.MakaleId == id).SingleOrDefault();
                if (null!=foto)
                {
                    if (System.IO.File.Exists(Server.MapPath(Guncellenecekmakale.Foto)))
                    {

                        System.IO.File.Delete(Server.MapPath(Guncellenecekmakale.Foto));
                    }
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoInfo.Extension;

                    img.Resize(800, 350);
                    img.Save("~/Uploads/MakaleFoto/" + newfoto);
                    Guncellenecekmakale.Foto = "/Uploads/MakaleFoto/" + newfoto.ToString();
                    Guncellenecekmakale.Baslik = makale.Baslik;
                    Guncellenecekmakale.Icerik = makale.Icerik;
                    Guncellenecekmakale.IsActive = true;
                    Guncellenecekmakale.KategoriId = makale.KategoriId;
                    db.SaveChanges();


                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminMakale/Delete/5
        public ActionResult Delete(int id)
        {
            var makale = db.Makale.Where(x => x.MakaleId == id).SingleOrDefault();

            if (null==makale)
            {
                return HttpNotFound();

            }
            else
            {
                return View(makale);
            }
          
        }

        // POST: AdminMakale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var silinecekmakale = db.Makale.Where(x => x.MakaleId == id).SingleOrDefault();

                if (null == silinecekmakale)
                {
                    return HttpNotFound();

                }
                // makalenin fotosunu silicem
                if (System.IO.File.Exists(Server.MapPath(silinecekmakale.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(silinecekmakale.Foto));
                }

                //yorum ve etıketlerı sılıcem
                foreach (var item in silinecekmakale.Yorum.ToList())
                {
                    db.Yorum.Remove(item);
                }

                foreach (var item in silinecekmakale.Etiket.ToList())
                {
                    db.Etiket.Remove(item);
                }
                db.Makale.Remove(silinecekmakale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
