using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class otel
    {
        [Key]
        public int otelID { get; set; }
        [Required(ErrorMessage = "Lütfen boş alanları doldurunuz..")]
        [Display(Name = "Otel Adı:")]
        public string otelAdi { get; set; }
        [Required(ErrorMessage = "Lütfen boş alanları doldurunuz..")]
        [Display(Name = "Kullanıcı Adı:")]
        public string otelkullaniciAdi { get; set; }
        [Required(ErrorMessage = "Lütfen boş alanları doldurunuz..")]
        [DataType(DataType.Password)]
        public string otelSifre { get; set; }       
        [Required(ErrorMessage = "Lütfen boş alanları doldurunuz..")]
        [Display(Name = "E-mail:")]
        public string otelEmail { get; set; }
        [Required(ErrorMessage = "Lütfen boş alanları doldurunuz..")]
        [Display(Name = "Telefon:")]
        public int otelTel { get; set; }
        [Required(ErrorMessage = "Lütfen boş alanları doldurunuz..")]
        public string sehir { get; set; }
        public string adres { get; set; }
        public string otelaciklama { get; set; }

        public string or { get; set; }
        [NotMapped]
        public HttpPostedFileBase resim { get; set; }
    }
}