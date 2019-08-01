using System.ComponentModel.DataAnnotations;

namespace SD.MvcWebUI.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email alanı zorunlu")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Güvenlik kodu alanı zorunlu")]
        public string SecurityCode { get; set; }
    }
}
