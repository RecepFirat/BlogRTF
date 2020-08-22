using RTFBLOG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTFBLOG.Controllers
{
   
    public class AdminController : Controller
    {
        private yeniBlogDbEntities db = new yeniBlogDbEntities();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.MakeleSayisi = db.Makale.Count();
            ViewBag.YorumSayisi = db.Yorum.Count();
            ViewBag.UyeSayisi = db.Uye.Count();
            ViewBag.KategoriSayisi = db.Kategori.Count();
            return View();
        }

    }
}