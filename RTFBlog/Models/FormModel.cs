using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RTFBLOG.Models
{
    public class FormModel
    {

        //bos gelirse sıkıntı olursa burada hata mesajını yazıcaz
        [Required(ErrorMessage ="Adı soyadı gereklidir!!!")]
        public string  AdiSoyadi { get; set; }
        [Required(ErrorMessage ="Boş Geçilemez")]
        [EmailAddress(ErrorMessage ="Geçerli bi email adresi giriniz")]
        public string  Email { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [MaxLength(500,ErrorMessage ="500 karakteri geçmeyiniz")]
        public string Mesaj { get; set; }

        //Kayıt Edilecekse MAil gonderene Geri Donus  Olacaksa
 
        public DateTime Tarih { get; set; }



    }
}