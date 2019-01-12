using RTFBLOG.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RTFBLOG.Controllers
{
    public class OtomatikMailController : Controller
    {
        // GET: OtomatikMail
        public ActionResult MailGonderme()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MailGonderme(FormModel model)
        {
            //mail icerigi
            MailMessage _mm = new MailMessage();
            _mm.SubjectEncoding = Encoding.Default;
            _mm.Subject = model.AdiSoyadi;
            _mm.BodyEncoding = Encoding.Default;
            _mm.Body = model.Mesaj;
            _mm.From = new MailAddress(ConfigurationManager.AppSettings["EmailAcount"]);
            _mm.To.Add("forpeople44@gmail.com");//bu mail adresine 
            

            //maili gonderen
            SmtpClient _smtpclient = new SmtpClient();
            _smtpclient.Host = ConfigurationManager.AppSettings["emailhost"];
            _smtpclient.Port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
            _smtpclient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAcount"], ConfigurationManager.AppSettings["EmailPassword"]); //Bu Mail Hesabi ile 
            _smtpclient.EnableSsl =true;//bool.Parse(ConfigurationManager.AppSettings["EmailsslEnable"]); 
       
            _smtpclient.Send(_mm);

            TempData["Basarili"] = "Mesaj basariyla gidmistir";

            return RedirectToAction("Index","Home");
        }
    }
}