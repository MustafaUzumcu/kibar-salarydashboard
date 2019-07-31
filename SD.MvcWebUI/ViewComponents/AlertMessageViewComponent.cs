using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD.MvcWebUI.Models;

namespace SD.MvcWebUI.ViewComponents
{
    public class AlertMessageViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var alerts = TempData.ContainsKey(AlertMessage.TempDataKey)
                ? (List<AlertMessage>)TempData[AlertMessage.TempDataKey]
                : new List<AlertMessage>();

            return View(alerts);
        }
    }
}
