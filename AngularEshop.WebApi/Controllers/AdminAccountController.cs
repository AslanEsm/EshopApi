using System.Threading.Tasks;
using AngularEshop.Common.Exceptions;
using AngularEshop.Common.Utilities;
using AngularEshop.Services.DTOs;
using AngularEshop.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Filters;

namespace AngularEshop.WebApi.Controllers
{
    [ApiResultFilter]
    public class AdminAccountController : SiteBaseController
    {
        #region Constructor

        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AdminAccountController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        #endregion Constructor

        #region Login

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> LogIn(LoginDTO model)
        {
            var user = await _userService.GetUserByUserName(model.UserName);
            var res = await _userService.LoginUserAsync(model, true);

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

                case LoginResult.NotAdmin:
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

            if (!await _userService.IsAdmin(user.Id))
                throw new BadRequestException("شما احازه دسترسی به این بخش را ندارید");

            return Ok(user);
        }

        #endregion CheckUserAuth

    }
}