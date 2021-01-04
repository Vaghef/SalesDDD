using Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesDDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDDD.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signinManager = signinManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (model.Password == null)
            {
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Mobile = model.Mobile,
                    AddedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (await _roleManager.FindByNameAsync("Customer") == null)
                    {
                        await _roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        });
                    }
                }
                await _userManager.AddToRoleAsync(user, "Customer");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "خطا در ذخیره کاربر" });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {

                RedirectToAction("Index", "Home");
                ViewData["ReturnUrl"] = returnUrl;
            }
            else
            {
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            }


            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

                if (result.Succeeded)
                {
                    //RedirectToLocal(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        public IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();

            return RedirectToAction(nameof(AccountController.Login), "Account");
        }
    }
}
