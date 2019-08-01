using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD.MvcWebUI.Models;

namespace SD.MvcWebUI.ViewComponents
{
    public class SideBarUserAreaViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new SideBarUserAreaViewModel
            {
                Email = HttpContext.User.Identity.Name
            };

            return View(model);
        }
    }
}
