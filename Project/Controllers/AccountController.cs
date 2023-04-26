using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        #region Fields
        private readonly UserService _userService;
        #endregion

        #region Constructors
        public AccountController(UserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Public members
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginInformation)
        {
            var error = _userService.TryLogin(loginInformation, out var token);
            return error == null ? Ok(new { token }) : BadRequest(error);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerInformation)
        {
            var success = _userService.Register(registerInformation);
            return success ? Ok() : BadRequest("Email is already in use");
        }
        #endregion
    }
}
