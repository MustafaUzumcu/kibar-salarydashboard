using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SD.MvcWebUI.Entities;
using SD.MvcWebUI.Models;
using SD.MvcWebUI.Services;

namespace SD.MvcWebUI.Controllers
{
    public class UserController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private AlertMessageService _alertMessageService;

        public UserController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, AlertMessageService alertMessageService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _alertMessageService = alertMessageService;
        }

        public IActionResult Index()
        {
            var identityUsers = _userManager.Users.ToList();
            var model = new UserViewModel
            {
                Users = new List<CustomUser>()
            };

            identityUsers.ForEach(u =>
                model.Users.Add(new CustomUser
                {
                    Id = u.Id,
                    Email = u.Email,
                    RoleNames = _userManager.GetRolesAsync(u).Result
                })
            );

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new UserAddViewModel
            {
                Roles = _roleManager.Roles.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserAddViewModel userAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                userAddViewModel.Roles = _roleManager.Roles.ToList();
                return View(userAddViewModel);
            }

            var user = new CustomIdentityUser
            {
                Email = userAddViewModel.Email,
                UserName = userAddViewModel.Email
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                userAddViewModel.Roles = _roleManager.Roles.ToList();
                ModelState.AddModelError("", "Kullanıcı oluşturulamadı");
                return View(userAddViewModel);
            }

            var selectedRoleNames = userAddViewModel.RoleNames;
            var roles = _roleManager.Roles.ToList();
            var addToRoles = roles.Where(q => selectedRoleNames.Contains(q.Name)).Select(q => q.Name).ToList();

            var roleResult = await _userManager.AddToRolesAsync(user, addToRoles);
            if (!roleResult.Succeeded)
            {
                ModelState.AddModelError("", "Seçtiğiniz roller kullanıcıya atanamadı.");
                return RedirectToAction("Index", "User");
            }

            return RedirectToAction("Index", "User");
        }
        
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _alertMessageService.AlertError("Hata", "Kullanıcı düzenlemek için id gerekli");
                return RedirectToAction("Index", "User");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _alertMessageService.AlertError("Hata", "Kullanıcı bulunamadı");
                return RedirectToAction("Index", "User");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var userEditViewModel = new UserEditViewModel
            {
                User = new CustomUser
                {
                    Id = user.Id,
                    Email = user.Email,
                    RoleNames = userRoles
                },
                RoleNames = userRoles,
                Roles = _roleManager.Roles.ToList()
            };

            return View(userEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel userEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                userEditViewModel.Roles = _roleManager.Roles.ToList();
                return View(userEditViewModel);
            }

            // Kullanıcıyı db den çek
            var identityUser = await _userManager.FindByIdAsync(userEditViewModel.User.Id);
            
            // Kullanıcının rollerini çek
            var userOldRoles = await _userManager.GetRolesAsync(identityUser);

            // Kullanıcı bilgilerini güncelle
            identityUser.Email = userEditViewModel.User.Email;
            identityUser.UserName = userEditViewModel.User.Email;
            var result = await _userManager.UpdateAsync(identityUser);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Kullanıcı güncellenemedi");
                userEditViewModel.Roles = _roleManager.Roles.ToList();
                return View(userEditViewModel);
            }

            // Kullanıcının eski rollerini sil
            var roleDeletes = await _userManager.RemoveFromRolesAsync(identityUser, userOldRoles);
            if (!roleDeletes.Succeeded)
            {
                ModelState.AddModelError("", "Kullanıcının rolleri güncellenirken hata meydana geldi.");
                userEditViewModel.Roles = _roleManager.Roles.ToList();
                return View(userEditViewModel);
            }

            // Kullanıcının rolünü güncelle
            var roleResult = await _userManager.AddToRolesAsync(identityUser, userEditViewModel.RoleNames);
            if (!roleResult.Succeeded)
            {
                ModelState.AddModelError("", "Kullanıcının rolleri güncellenemedi");
                userEditViewModel.Roles = _roleManager.Roles.ToList();
                return View(userEditViewModel);
            }

            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _alertMessageService.AlertError("Hata", "Kullanıcı silmek için id gerekli");
                return RedirectToAction("Index", "User");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _alertMessageService.AlertError("Hata", "Kullanıcı bulunamadı");
                return RedirectToAction("Index", "User");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                _alertMessageService.AlertError("Hata", "Kullanıcı silinemedi");
                return RedirectToAction("Index", "User");
            }

            return RedirectToAction("Index", "User");
        }
    }
}