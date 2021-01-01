using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesDDD.Helper;
using SalesDDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesDDD.Controllers
{
    public class UserController : BaseController
    {
        private IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
                 
        }
        public IActionResult Index()
        {
            var model = _userManager.Users.Select(x => new UserViewModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Mobile = x.Mobile,
                UserName = x.UserName,
                Id = x.Id
            });
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Claims = typeof(AccessClaims).GetFields().Select(x => new KeyValueViewModel { Key = x.Name, Value = x.GetValue(new AccessClaims()).ToString() }).ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (model.Password == null)
                return View(model);

            if(ModelState.IsValid)
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

                if(result.Succeeded)
                {
                    if(await _roleManager.FindByNameAsync(model.RoleType.ToString()) == null)
                    {
                        await _roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = model.RoleType.ToString(),
                            NormalizedName = model.RoleType.ToString().ToUpper()
                        });
                    }

                    await _userManager.AddToRoleAsync(user, model.RoleType.ToString());

                    if(model.Claims.Count > 0)
                    {
                        var claims = model.Claims.Select(x => new Claim(nameof(AccessClaims), x));

                        await _userManager.AddClaimsAsync(user, claims);
                    }

                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Claims = typeof(AccessClaims).GetFields().Select(x => new KeyValueViewModel { Key = x.Name, Value = x.GetValue(new AccessClaims()).ToString() }).ToList();

            var item = await _userManager.FindByIdAsync(id);

            if(item != null)
            {
                var model = _mapper.Map<ApplicationUser, UserViewModel>(item);

                var roles = await _userManager.GetRolesAsync(item);

                if (roles.Any())
                    model.RoleType = Enum.Parse<RoleType>(roles.FirstOrDefault());

                model.Claims = (await _userManager.GetClaimsAsync(item)).Where(x => x.Type == nameof(AccessClaims)).Select(x => x.Value).ToList();

                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (model.Password == null)
                return View(model);

            if (ModelState.IsValid)
            {

                var userFounded = await _userManager.FindByIdAsync(model.Id);

                if(userFounded != null)
                {
                    userFounded.UserName = model.UserName;
                    userFounded.FirstName = model.FirstName;
                    userFounded.LastName = model.LastName;
                    userFounded.Email = model.Email;
                    userFounded.PhoneNumber = model.PhoneNumber;
                    userFounded.EmailConfirmed = true;
                    userFounded.PhoneNumberConfirmed = true;
                    userFounded.Mobile = model.Mobile;
                    userFounded.LastModified = DateTime.Now;
                }

                if(!string.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(userFounded);
                    await _userManager.ResetPasswordAsync(userFounded, token, model.Password);
                }

                await _userManager.UpdateAsync(userFounded);

                var user = await _userManager.FindByIdAsync(model.Id);
                var roles = await _userManager.GetRolesAsync(user);               

                await _userManager.RemoveFromRolesAsync(user, roles);

                if (await _roleManager.FindByNameAsync(model.RoleType.ToString()) == null)
                {
                    await _roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = model.RoleType.ToString(),
                        NormalizedName = model.RoleType.ToString().ToUpper()
                    });
                }

                await _userManager.AddToRoleAsync(user, model.RoleType.ToString());


                await _userManager.RemoveClaimsAsync(user, (await _userManager.GetClaimsAsync(user)));
                if (model.Claims.Count > 0)
                {
                    var claims = model.Claims.Select(x => new Claim(nameof(AccessClaims), x));

                    await _userManager.AddClaimsAsync(user, claims);
                }

                SuccessMessage = Resources.Messages.ChangesSavedSuccessfully;

                return RedirectToAction("Edit", new { id = userFounded.Id });
            }
            ErrorMessage = "اطلاعات کاربر جاری نامعتبر است";
            return View(model);
        }
    }
}
