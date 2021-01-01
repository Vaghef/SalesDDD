using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesDDD.Helper
{
    public class AccessClaims
    {
        public readonly string AccessToBaseInfo = "اطلاعات پایه";
        public readonly string AccessToUsers = "کاربران";
        public readonly string AccessToProducts = "محصولات";
        public readonly string AccessToReport = "گزارش";
    }
    public static class Utility
    {
        public static async Task<bool> HasAccess(this ClaimsPrincipal User, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, string access)
        {
            if (User.IsInRole("Admin"))
                return true;

            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var claims = await userManager.GetClaimsAsync(user);

            return claims.Any(x => x.Value == access);
        }
    }
}
