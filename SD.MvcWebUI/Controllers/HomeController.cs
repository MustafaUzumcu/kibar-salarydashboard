using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD.Business.Abstract;
using SD.MvcWebUI.Models;
using SD.MvcWebUI.Services;

namespace SD.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        private ISystemParameterService _systemParameterService;
        private AlertMessageService _alertMessageService;

        public HomeController(ISystemParameterService systemParameterService, AlertMessageService alertMessageService)
        {
            _systemParameterService = systemParameterService;
            _alertMessageService = alertMessageService;
        }

        public IActionResult Index()
        {
            List<SystemParameterViewModel> parameterViewModels = new List<SystemParameterViewModel>();

            for (int i = 0; i < 100; i++)
            {
                parameterViewModels.Add(new SystemParameterViewModel
                {
                    ParameterId = i,
                    ParameterName = $"Parameter Name {i}",
                    ParameterValue = $"Parameter Value {i}",
                    Description = $"Parameter Description {i}",
                    IsReadOnly = i % 2 == 0,
                });
            }

            return View(parameterViewModels);

            // Example Get Data From Database
            //var parameters = _systemParameterService.GetList();
            //string result = "Parametre eklenmedi";
            //if (parameters.Count > 0)
            //{
            //    result =
            //        $"{parameters[0].ParameterName} : {parameters[0].ParameterValue} ({parameters[0].Description})";
            //}
            //return result;
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult About(string name)
        {
            _alertMessageService.AlertError("Hata", "Hata mesajı örneği " + name);
            return View();
        }
    }
}