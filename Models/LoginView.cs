using System.ComponentModel.DataAnnotations;

namespace NewBlogApp.Models
{
    public class LoginView
    {
        [Required (ErrorMessage = "Eposta alanı zorunludur")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        [StringLength(10, ErrorMessage = "Şifre en az {2} karakter olmalıdır.", MinimumLength = 6)]
        public string? Password { get; set; }
    }
}
