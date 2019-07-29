using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD.Business.Abstract;
using SD.MvcWebUI.Models;

namespace SD.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        private ISystemParameterService _systemParameterService;

        public HomeController(ISystemParameterService systemParameterService)
        {
            _systemParameterService = systemParameterService;
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
    }
}