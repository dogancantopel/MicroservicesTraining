using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class SignInInput
    {
        [Display(Name ="Email Adresiniz")]
        [Required]
        public string Email { get; set; }

        [Display(Name ="Şifre")]
        [Required]
        public string Password { get; set; }
        [Display(Name ="Beni Hatırla")]
        public bool IsRemember { get; set; }
    }
}
