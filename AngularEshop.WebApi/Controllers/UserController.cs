using AngularEshop.Entities.Account;
using AngularEshop.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFramework.Filters;

namespace AngularEshop.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiResultFilter]
    public class UserController : SiteBaseController
    {
        #region Constructor

        private readonly IUserService _userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            this.logger = logger;
        }

        #endregion Constructor

        #region GetAllUsers
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return users;
        }

        #endregion GetAllUsers

        #region GetUserById
        [AllowAnonymous]
        [HttpGet]
        [Route("GetUserById/{id:int}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user;
        }

        #endregion GetUserById
    }
}