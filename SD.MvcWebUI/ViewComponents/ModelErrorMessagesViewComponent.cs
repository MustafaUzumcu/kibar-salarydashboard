using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SD.MvcWebUI.Models;

namespace SD.MvcWebUI.ViewComponents
{
    public class ModelErrorMessagesViewComponent:ViewComponent
    {
        public ViewViewComponentResult Invoke(ModelStateDictionary modelStateDictionary)
        {
            string validationErrors = string.Join(", ",
                modelStateDictionary.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v=> v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray()
            );

            var model = new ModelErrorViewModel
            {
                Message = validationErrors
            };

            return View(model);
        }
    }
}
