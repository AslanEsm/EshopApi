using AngularEshop.Services.Intefaces;

using WebFramework.Filters;

namespace AngularEshop.WebApi.Controllers
{
    [ApiResultFilter]
    public class TestController : SiteBaseController
    {
        private readonly IUserService _userService;

        public TestController(IUserService userService)
        {
            _userService = userService;
        }
    }
}