using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Orders
{
    public class CheckOutInfoInput
    {
        [Display(Name ="İl")]
        public string Province { get; set; }
        [Display(Name = "İlçe")]
        public string District { get; set; }
        [Display(Name = "Cadde")]
        public string Street { get; set; }
        [Display(Name = "Posta kodu")]
        public string ZipCode { get; set; }
        [Display(Name = "Adres")]
        public string Description { get; set; }

        [Display(Name = "Kart İsim soyad")]
        public string CardName { get; set; }
        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }
        [Display(Name = "Son Kullanma Tarihi")]
        public string Expiration { get; set; }
        [Display(Name = "Cvv")]
        public string Cvv { get; set; }
    }
}
