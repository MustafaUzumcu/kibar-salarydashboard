using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD.Business.Abstract;

namespace SD.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        private ISystemParameterService _systemParameterService;

        public HomeController(ISystemParameterService systemParameterService)
        {
            _systemParameterService = systemParameterService;
        }

        public string Index()
        {
            // Example Get Data From Database
            var parameters = _systemParameterService.GetList();
            string result = "Parametre eklenmedi";
            if (parameters.Count > 0)
            {
                result =
                    $"{parameters[0].ParameterName} : {parameters[0].ParameterValue} ({parameters[0].Description})";
            }
            return result;
        }
    }
}