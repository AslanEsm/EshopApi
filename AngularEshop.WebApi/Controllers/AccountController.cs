using AngularEshop.Common.Exceptions;
using AngularEshop.Common.Utilities;
using AngularEshop.Entities.Account;
using AngularEshop.Services.DTOs;
using AngularEshop.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebFramework.Filters;

namespace AngularEshop.WebApi.Controllers
{
    [ApiResultFilter]
    public class AccountController : SiteBaseController
    {
        #region Constructor

        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AccountController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        #endregion Constructor

        #region SignUp

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(RegisterDTO model)
        {
            var res = await _userService.RegisterUserAsync(model);

            switch (res)
            {
                case RegisterResult.EmailExists:
                    return BadRequest(RegisterResult.EmailExists.ToDisplay());

                case RegisterResult.UserNameExists:
                    return BadRequest(RegisterResult.UserNameExists.ToDisplay());

                case RegisterResult.ServerError:
                    throw new LogicException(RegisterResult.ServerError.ToDisplay());
            }

            return Ok();
        }

        #endregion SignUp

        #region Login

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> LogIn(LoginDTO model)
        {
            var user = await _userService.GetUserByUserName(model.UserName);
            var res = await _userService.LoginUserAsync(model);

            switch (res)
            {
                case LoginResult.InCorrectData:
                    return BadRequest(LoginResult.InCorrectData.ToDisplay());

                case LoginResult.RequiresTwoFactor:
                    throw new BadRequestException(LoginResult.RequiresTwoFactor.ToDisplay());

                case LoginResult.IsNotAllowed:
                    throw new BadRequestException(LoginResult.IsNotAllowed.ToDisplay());

                case LoginResult.IsLockedOut:
                    throw new BadRequestException(LoginResult.IsLockedOut.ToDisplay());

                case LoginResult.NotActivated:
                    throw new BadRequestException(LoginResult.NotActivated.ToDisplay());

                case LoginResult.Success:
                    var token = await _jwtService.GenerateAsync(user);
                    return Ok(new TokenDto()
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = token.Token,
                        Expiration = token.Expiration
                    });
            }

            return Ok();
        }

        #endregion Login

        #region CheckUserAuth

        [HttpPost]
        [Route("check-auth")]
        public async Task<IActionResult> CkeckUserAuth()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
                throw new BadHttpRequestException("کاربر لاگین نیست");

            return Ok(user);
        }

        #endregion CheckUserAuth

        #region EditUser

        [HttpPost]
        [Route("edit-user")]
        public async Task<ActionResult<User>> EditUser([FromBody] EditUserDto editUser)
        {
            if (!User.Identity.IsAuthenticated)
                throw new BadHttpRequestException("لطف وارد سایت شوید");

            var userId = int.Parse(User.Identity.GetUserId());

            var user = await _userService.EditUserInfo(editUser, userId);

            return Ok(user);
        }

        #endregion EditUser
    }
}