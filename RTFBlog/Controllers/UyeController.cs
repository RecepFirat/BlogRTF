using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RTFBLOG.Controllers;
using RTFBLOG.Models;
namespace RTFBLOG.Controllers
{
    public class UyeController : Controller
    {

        yeniBlogDbEntities db = new yeniBlogDbEntities();
        // GET: Uye 
        public ActionResult Index( int id)
        {
            var uye = db.Uye.Where(x => x.UyeId == id).SingleOrDefault();

            if (Convert.ToInt32(Session["uyeid"])!=uye.UyeId)
            {
                return HttpNotFound();

            }

            return View(uye);
        }
        public ActionResult Login()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Login(string KullaniciAdi,string Sifre,string email)
        {
            if (KullaniciAdi!=null && Sifre !=null && email !=null)
            {
                var login = db.Uye.Where(u => u.KullaniciAdi == KullaniciAdi).SingleOrDefault();
                if (login.KullaniciAdi.ToString() == KullaniciAdi.ToString() && login.Email.ToString() == email.ToString() && login.Sifre.ToString() == Sifre.ToString())
                {
                    Session["uyeid"] = login.UyeId;
                    Session["kullaniciadi"] = login.KullaniciAdi;
                    Session["yetkiid"] = login.YetkiId;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Uyari = "Kullanıcı adi, Mail yada sifrenizi kontrol ediniz.";
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session["kullaniciadi"] = null;
            Session["yetkiid"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Uye uye, HttpPostedFileBase foto,string sifre)//foto yazdıgım kısmı idsi foto olucak
        {
//            @model RTFBLOG.Models.Uye

//@{
//                ViewBag.Title = "Create";
//                Layout = "~/Views/Shared/_Layout.cshtml";
//            }
//< p class="alert">
//    @if(ViewBag.data != null)
//        {
//            @ViewBag.data
//    }

//</p>
//<h2>Yeni Üye Kayıt</h2>
//@*video 11 uye kayıt ıslemleri*@
//@using(Html.BeginForm("Create", "Uye", FormMethod.Post, new { enctype = "multipart/form-data" }))
//{
//    @Html.AntiForgeryToken()

//    <div class="form-horizontal">

//        <hr />
//        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
//        <div class="form-group">
//            @Html.LabelFor(model => model.KullaniciAdi, htmlAttributes: new { @class = "control-label col-md-2" })
//            <div class="col-md-10">
//                @Html.EditorFor(model => model.KullaniciAdi, new { htmlAttributes = new { @class = "form-control" } })
//                @Html.ValidationMessageFor(model => model.KullaniciAdi, "", new { @class = "text-danger" })
//            </div>
//        </div>

//        <div class="form-group">
//            @Html.LabelFor(model => model.Email, htmlAttributes: new { @class = "control-label col-md-2" })
//            <div class="col-md-10">
//                @Html.EditorFor(model => model.Email, new { htmlAttributes = new { @class = "form-control", @type = "email" } })
//                @Html.ValidationMessageFor(model => model.Email, "", new { @class = "text-danger" })
//            </div>
//        </div>

//        <div class="form-group">
//            @Html.LabelFor(model => model.Sifre, htmlAttributes: new { @class = "control-label col-md-2" })
//            <div class="col-md-10">
//                @Html.EditorFor(model => model.Sifre, new { htmlAttributes = new { @class = "form-control", @type = "password" } })
//                @Html.ValidationMessageFor(model => model.Sifre, "", new { @class = "text-danger" })
//            </div>
//        </div>
//        <div class="form-group">
//            @Html.LabelFor(model => model.Sifre, htmlAttributes: new { @class = "control-label col-md-2" })
//            <div class="col-md-10">
//                <input type = "password" class="form-control" name="sifre" />
//            </div>
//        </div>

//        <div class="form-group">
//            @Html.LabelFor(model => model.AdSoyad, htmlAttributes: new { @class = "control-label col-md-2" })
//            <div class="col-md-10">
//                @Html.EditorFor(model => model.AdSoyad, new { htmlAttributes = new { @class = "form-control" } })
//                @Html.ValidationMessageFor(model => model.AdSoyad, "", new { @class = "text-danger" })
//            </div>
//        </div>

//        <div class="form-group">
//            @Html.LabelFor(model => model.Foto, htmlAttributes: new { @class = "control-label col-md-2" })
//            <div class="col-md-10">
//                @* @Html.EditorFor(model => model.Foto, new { htmlAttributes = new { @class = "form-control" } })*@
//                <input type = "file" name="Foto" id="Foto" accept=".jpgi.png,.gif,.JPEG,.jpeg" class="form-control" value="" />
//                @Html.ValidationMessageFor(model => model.Foto, "", new { @class = "text-danger" })
//            </div>
//        </div>
//        @*<div class="form-group">
//                @Html.LabelFor(model => model.Foto, htmlAttributes: new { @class = "control-label col-md-2" })
//                <div class="col-md-10">
//                    @Html.EditorFor(model => model.Foto, new { htmlAttributes = new { @class = "form-control" } })
//                    @Html.ValidationMessageFor(model => model.Foto, "", new { @class = "text-danger" })
//                </div>
//            </div>*@

//        @*<div class="form-group">
//                @Html.LabelFor(model => model.YetkiId, "YetkiId", htmlAttributes: new { @class = "control-label col-md-2" })
//                <div class="col-md-10">
//                    @Html.DropDownList("YetkiId", null, htmlAttributes: new { @class = "form-control" })
//                    @Html.ValidationMessageFor(model => model.YetkiId, "", new { @class = "text-danger" })
//                </div>
//            </div>*@

//        <div class="form-group">
//            <div class="col-md-offset-2 col-md-10">
//                <input type = "submit" value="Üye ol" class="btn btn-default" />
//            </div>
//        </div>
//    </div>
//}




            if (ModelState.IsValid)
            {
                if (uye.Sifre==sifre)
                {
                   //todo
                if (foto != null)
                {
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoInfo.Extension;

                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uye.Foto = "/Uploads/UyeFoto/" + newfoto.ToString();
                    uye.Aktif = false;
                    uye.YetkiId = 2;
                    db.Uye.Add(uye);
                    db.SaveChanges();
                        
                    Session["uyeid"] = uye.UyeId;
                    Session["kullaniciadi"] = uye.KullaniciAdi;

                    ViewBag.data = "üye kaydınızın talebi alınmıstır hesabınız aktiflestiğinde bilgilendirileceksiniz";
                    return View();

                }
                else
                {
                        ViewBag.data = "Fotograf yüklenmedi";
                }

                }
                else
                {
                    ViewBag.data = "Sifreler Uyuşmamaktadir";
                }
            }
            return View();

        }

        public ActionResult Edit(int id )
        {
            var uye = db.Uye.Where(x => x.UyeId == id).SingleOrDefault();

            if (Convert.ToInt32(Session["uyeid"])!=uye.UyeId)
            {
                return HttpNotFound();
            }
            return View(uye);

        }

        [HttpPost]
        public ActionResult Edit(Uye uye,int id ,HttpPostedFileBase foto)
        {
            if (ModelState.IsValid)
            {
                var Guncellenecekuye = db.Uye.Where(x => x.UyeId == id).SingleOrDefault();
                if (null != foto)
                {
                    if (System.IO.File.Exists(Server.MapPath(Guncellenecekuye.Foto)))
                    {

                        System.IO.File.Delete(Server.MapPath(Guncellenecekuye.Foto));
                    }
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoInfo = new FileInfo(foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoInfo.Extension;

                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    Guncellenecekuye.Foto = "/Uploads/UyeFoto/" + newfoto.ToString();
                }
                Guncellenecekuye.AdSoyad = uye.AdSoyad;
                    Guncellenecekuye.Email = uye.Email;
                    Guncellenecekuye. KullaniciAdi= uye.KullaniciAdi;
                    Guncellenecekuye. Sifre = uye.Sifre;
                    db.SaveChanges();
                    Session["kullaniciadi"] = uye.KullaniciAdi;
                    return RedirectToAction("Index", "Home", new { id = uye.UyeId });//bu sekılde parametresınıde gonderebılıyoruz
               
            }
            return View();


        }

        public ActionResult UyeProfil(int id)
        {

            var uye = db.Uye.Where(x => x.UyeId == id).SingleOrDefault();
            return View(uye);



        }
    }
}