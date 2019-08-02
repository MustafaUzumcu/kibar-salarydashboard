using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SD.Business.Abstract;
using SD.MvcWebUI.Models;
using SD.MvcWebUI.Services;

namespace SD.MvcWebUI.Controllers
{
    
    public class HomeController : Controller
    {
        private AlertMessageService _alertMessageService;

        public HomeController(AlertMessageService alertMessageService)
        {
            _alertMessageService = alertMessageService;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}