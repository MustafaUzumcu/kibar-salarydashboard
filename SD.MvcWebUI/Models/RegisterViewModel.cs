using System.ComponentModel.DataAnnotations;

namespace SD.MvcWebUI.Models
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email alanı zorunlu", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunlu", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
