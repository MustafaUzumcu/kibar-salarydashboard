using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SD.MvcWebUI.Entities;
using SD.MvcWebUI.Models;

namespace SD.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private SignInManager<CustomIdentityUser> _signInManager;

        public AccountController(
            UserManager<CustomIdentityUser> userManager,
            RoleManager<CustomIdentityRole> roleManager,
            SignInManager<CustomIdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                CustomIdentityUser user = new CustomIdentityUser
                {
                    Email = registerViewModel.Email
                };

                var userCreateResult = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (!userCreateResult.Succeeded)
                {
                    ModelState.AddModelError("", "Kullanıcı oluşturulamadı");
                    return View(registerViewModel);
                }
                else
                {
                    _userManager.AddToRoleAsync(user, "User").Wait();
                    return RedirectToAction("Login", "Account");
                }
            }

            return View(registerViewModel);
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var loginResult = await _signInManager.PasswordSignInAsync
            (
                loginViewModel.Email,
                loginViewModel.Password,
                loginViewModel.RememberMe,
                false
            );

            if (loginResult.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Mail adresi veya parola yanlış");
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email alanı zorunlu");
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı");
                return View();
            }

            var token = _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPasswordUrl = Url.Action("ResetPassword", "Account", new
            {
                token
            });

            // TODO: Mail Send reset password url to user email

            Console.WriteLine(resetPasswordUrl);

            return RedirectToAction("SendedResetMail");
        }

        public IActionResult SendedResetMail()
        {
            return View();
        }

        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordViewModel { Token = token };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordModel);
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı");
                return View(resetPasswordModel);
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return View(resetPasswordModel);
        }
    }
}