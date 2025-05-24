using System.ComponentModel.DataAnnotations;

namespace NewBlogApp.Models
{
    public class RegisterView
    {
        [Required(ErrorMessage = "Kullanıcı adı  alanı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur")]
        [Display(Name = "Ad Soyad")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email alanı boş bırakılamaz")]
        [EmailAddress]
        [Display(Name = "Email")]
        
        public string? Email { get; set; }


        [Required (ErrorMessage="Şifre alanı boş bırakılamaz")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        [StringLength(10, ErrorMessage = "Şifre en az {2} karakter olmalıdır.", MinimumLength = 6)]
        
        public string? Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor.")]
        public string? ConfirmPassword { get; set; }
    }
}
