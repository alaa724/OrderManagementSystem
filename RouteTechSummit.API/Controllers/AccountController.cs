using AutoMapper;
using Core.Entities.Identity;
using Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using RouteTechSummit.API.DTO._Identity;
using System.Security.Claims;

namespace RouteTechSummit.API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<SystemUser> _userManager;
        private readonly SignInManager<SystemUser> _signInManager;
        private readonly IAuthServices _authService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<SystemUser> userManager,
            SignInManager<SystemUser> signInManager,
            IAuthServices authService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")] // POST : /api/Account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return Unauthorized(new ApiResponse(401, "Invalid Login"));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Login"));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpPost("register")] // POST : /api/Account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new SystemUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                PhoneNumber = model.Phone
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return Unauthorized(new ApiValidationErrorResponse() { Errors = result.Errors.Select(E => E.Description) });

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet] // GET : /api/account
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user?.DisplayName ?? string.Empty,
                Email = user?.Email ?? string.Empty,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }


    }
}
