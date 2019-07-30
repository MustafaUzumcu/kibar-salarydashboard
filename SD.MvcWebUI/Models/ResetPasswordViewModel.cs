using System.ComponentModel.DataAnnotations;

namespace SD.MvcWebUI.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Token bulunamadı")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Email alanı zorunlu")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunlu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
