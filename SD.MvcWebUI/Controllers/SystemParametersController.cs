using Microsoft.AspNetCore.Mvc;
using SD.Business.Abstract;
using SD.Entities.Concrete;
using SD.MvcWebUI.Models;
using SD.MvcWebUI.Services;

namespace SD.MvcWebUI.Controllers
{
    public class SystemParametersController : Controller
    {
        private ISystemParameterService _systemParameterService;
        private AlertMessageService _alertMessageService;

        public SystemParametersController(ISystemParameterService systemParameterService, AlertMessageService alertMessageService)
        {
            _systemParameterService = systemParameterService;
            _alertMessageService = alertMessageService;
        }


        public IActionResult Index()
        {
            var model = new SystemParameterListModel
            {
                SystemParameters = _systemParameterService.GetList()
            };

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new SystemParameterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SystemParameterViewModel systemParameterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(systemParameterViewModel);
            }

            // TODO: Hata yönetimi
            _systemParameterService.Add(new SystemParameter
            {
                ParameterName = systemParameterViewModel.ParameterName,
                ParameterValue = systemParameterViewModel.ParameterValue,
                Description = systemParameterViewModel.Description,
                IsReadOnly = systemParameterViewModel.IsReadOnly
            });

            return RedirectToAction("Index", "SystemParameters");
        }

        public IActionResult Edit(int id)
        {
            var systemParameter = _systemParameterService.Get(q => q.ParameterId == id);
            if (systemParameter == null)
            {
                _alertMessageService.AlertError("Hata", "Sistem parametresi bulunamadı");
                return RedirectToAction("Index", "SystemParameters");
            }

            var model = new SystemParameterViewModel
            {
                ParameterId = systemParameter.ParameterId,
                ParameterName = systemParameter.ParameterName,
                ParameterValue = systemParameter.ParameterValue,
                Description = systemParameter.Description,
                IsReadOnly = systemParameter.IsReadOnly
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SystemParameterViewModel systemParameterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(systemParameterViewModel);
            }

            var systemParameter = new SystemParameter
            {
                ParameterId = systemParameterViewModel.ParameterId,
                ParameterName = systemParameterViewModel.ParameterName,
                ParameterValue = systemParameterViewModel.ParameterValue,
                Description = systemParameterViewModel.Description,
                IsReadOnly = systemParameterViewModel.IsReadOnly
            };

            // TODO: Hata yönetimi
            _systemParameterService.Update(systemParameter);

            return RedirectToAction("Index", "SystemParameters");
        }


        public IActionResult Delete(int id)
        {
            var systemParameter = _systemParameterService.Get(q => q.ParameterId == id);
            if (systemParameter == null)
            {
                _alertMessageService.AlertError("Hata", "Sistem parametresi bulunamadı");
                return RedirectToAction("Index", "SystemParameters");
            }

            // TODO: Hata yönetimi
            _systemParameterService.Delete(systemParameter);

            return RedirectToAction("Index", "SystemParameters");
        }
    }
}