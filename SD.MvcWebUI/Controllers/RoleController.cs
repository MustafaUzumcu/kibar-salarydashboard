using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD.MvcWebUI.Entities;
using SD.MvcWebUI.Models;
using SD.MvcWebUI.Services;

namespace SD.MvcWebUI.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<CustomIdentityRole> _roleManager;
        private AlertMessageService _alertMessageService;

        public RoleController(RoleManager<CustomIdentityRole> roleManager, AlertMessageService alertMessageService)
        {
            _roleManager = roleManager;
            _alertMessageService = alertMessageService;
        }

        public IActionResult Index()
        {
            var model = new RoleViewModel
            {
                Roles = _roleManager.Roles.ToList()
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View(new RoleAddViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleAddViewModel roleAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Rol adı zorunlu.");
                return View(roleAddViewModel);
            }

            var role = new CustomIdentityRole
            {
                Name = roleAddViewModel.RoleName
            };

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Rol eklenemedi.");
                return View(roleAddViewModel);
            }

            return RedirectToAction("Index", "Role");
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _alertMessageService.AlertError("Hata", "Rol düzenlemek için id gerekli");
                return RedirectToAction("Index", "Role");
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                _alertMessageService.AlertError("Hata", "Rol bulunamadı");
                return RedirectToAction("Index", "Role");
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomIdentityRole role)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Rol adı zorunlu");
            }

            var identityRole = await _roleManager.FindByIdAsync(role.Id);
            identityRole.Name = role.Name;
            
            var result = await _roleManager.UpdateAsync(identityRole);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Rol güncellenemedi");
                return View(role);
            }

            return RedirectToAction("Index", "Role");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _alertMessageService.AlertError("Hata", "Rol silmek için id gerekli");
                return RedirectToAction("Index", "Role");
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                _alertMessageService.AlertError("Hata", "Rol bulunamadı");
                return RedirectToAction("Index", "Role");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                _alertMessageService.AlertError("Hata", "Rol silinemedi");
            }

            return RedirectToAction("Index", "Role");
        }
    }
}