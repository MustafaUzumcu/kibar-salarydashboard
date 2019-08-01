using System.ComponentModel.DataAnnotations;

namespace SD.MvcWebUI.Models
{
    public class RoleAddViewModel
    {
        [Required(ErrorMessage = "Rol alanı zorunlu")]
        public string RoleName { get; set; }
    }
}
