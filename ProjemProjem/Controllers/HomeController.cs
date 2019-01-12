using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using RTFBLOG.Models;
namespace RTFBLOG.Controllers
{
    public class HomeController : Controller
    {
        u8417622_dbblgEntities db = new u8417622_dbblgEntities();
        // GET: Home
        public ActionResult Index(int Page=1)
        {

            var makale = db.Makale.OrderByDescending(m => m.MakaleId).ToPagedList(Page, 5);
            return View(makale);
        }
        public ActionResult KategoriMakale(int id) {
            var makaleler = db.Makale.OrderByDescending(x => x.Kategori.KategoriId).Where(x => x.Kategori.KategoriId == id);
            return View(makaleler);
        }
        public ActionResult BlogAra(string Ara = null)
        {
            var Aranan = db.Makale.Where(x => x.Baslik.Contains(Ara)).ToList();
            return View(Aranan.OrderByDescending(x=>x.Tarih));
        }

        public ActionResult SonYorumlarPartial()
        {

            return View(db.Yorum.OrderByDescending(x=>x.YorumId).Take(5));
        }
        public ActionResult PopulerMakalelerPartial() {



            return View(db.Makale.OrderByDescending(x => x.Okunma).Take(5));

        }
        public ActionResult MakaleDetay(int id)
        {

            var makale = db.Makale.Where(x => x.MakaleId == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }

            return View(makale);
        }

        public ActionResult Hakkimizda()
        {


            return View();

        }

        public ActionResult Iletisim()
        {
            return View();
        }

        public ActionResult KategoriPartial()
        {

            var data = db.Kategori.ToList();

            return View(db.Kategori.ToList());
        }

        public JsonResult YorumYap(string yorum, int Makaleid)
        {

            var kullaniciid = Session["uyeid"];
            if (yorum!=null)
            {
                db.Yorum.Add(new Yorum
                {

                    UyeId = Convert.ToInt32(kullaniciid),
                    MakaleId = Makaleid,
                    Icerik = yorum,
                    Tarih = DateTime.Now,



                });
                db.SaveChanges();

            }
            return Json(false,JsonRequestBehavior.AllowGet);//isteklere izin veriuyoruz

        }

        public ActionResult YorumSil(int id)
        {
            var uyeid = Session["uyeid"];
            var yorum = db.Yorum.Where(x => x.YorumId == id).SingleOrDefault();
            var Makale = db.Makale.Where(x => x.MakaleId == yorum.MakaleId).SingleOrDefault();

            if (yorum.UyeId==Convert.ToInt32(uyeid))
            {
                db.Yorum.Remove(yorum);
                db.SaveChanges();
                //bu sekildeded parametre atabiliyoruz
                return RedirectToAction("MakaleDetay", "Home", new { id = Makale.MakaleId });
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult OkunmaArttir(int Makaleid) {

            var makale = db.Makale.Where(x => x.MakaleId == Makaleid).SingleOrDefault();
            makale.Okunma += 1;
            db.SaveChanges(); 

            return View();
        }
    }
}