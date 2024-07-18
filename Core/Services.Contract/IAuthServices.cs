using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Contract
{
    public interface IAuthServices
    {
        Task<string> CreateTokenAsync(SystemUser user, UserManager<SystemUser> userManager);
    }
}
