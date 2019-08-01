using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SD.MvcWebUI.Entities;

namespace SD.MvcWebUI.Models
{
    public class UserAddViewModel
    {
        [Required(ErrorMessage = "Email alanı zorunlu")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Rol alanı zorunlu")]
        public IList<string> RoleNames { get; set; }

        public List<CustomIdentityRole> Roles { get; set; }
    }
}
