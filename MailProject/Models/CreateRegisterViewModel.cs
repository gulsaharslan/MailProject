using System.ComponentModel.DataAnnotations;

namespace MailProject.Models
{
    public class CreateRegisterViewModel
    {
        [Display(Name="Ad")]
        [Required(ErrorMessage="Lütfen ad giriniz")]
        public string Name { get; set; }

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Lütfen soyad giriniz")]
        public string Surname { get; set; }


        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage="Şifreler uyuşmuyor")]
        [Required(ErrorMessage = "Lütfen şifreyi tekrar giriniz")]

        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Lütfen email giriniz")]
        public string Mail { get; set; }

        [Display(Name = "Kullanıcı adı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adı giriniz")]
        public string UserName { get; set; }
    }
}
