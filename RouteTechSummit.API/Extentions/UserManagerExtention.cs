using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace RouteTechSummit.API.Extentions
{
    public static class UserManagerExtention
    {
        public static async Task<SystemUser> FindUserWithAddressAsync(this UserManager<SystemUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.FirstOrDefaultAsync(U => U.NormalizedEmail == email.ToUpper());

            return user;

        }
    }
}
