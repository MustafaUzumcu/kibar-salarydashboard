using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SD.MvcWebUI.Entities;

namespace SD.MvcWebUI.Models
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Kullanıcı alanı zorunlu")]
        public CustomUser User { get; set; }

        [Required(ErrorMessage = "Rol alanı zorunlu")]
        public IList<string> RoleNames { get; set; }

        public List<CustomIdentityRole> Roles { get; set; }
    }
}
