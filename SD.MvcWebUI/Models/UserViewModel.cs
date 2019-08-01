using System.Collections.Generic;

namespace SD.MvcWebUI.Models
{
    public class UserViewModel
    {
        public List<CustomUser> Users { get; set; }
    }

    public class CustomUser
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public IList<string> RoleNames { get; set; }
    }
}
