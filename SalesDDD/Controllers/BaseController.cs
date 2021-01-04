using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesDDD.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public UserManager<ApplicationUser> _userManager;
        [TempData]
        public string ErrorMessage { get; set; }
        [TempData]
        public string SuccessMessage { get; set; }
        public string UserId { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserId = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            base.OnActionExecuting(context);
        }

    }
}
