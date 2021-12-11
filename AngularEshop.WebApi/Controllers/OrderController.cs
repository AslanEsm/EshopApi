using AngularEshop.Common.Utilities;
using AngularEshop.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Filters;

namespace AngularEshop.WebApi.Controllers
{
    [ApiResultFilter]
    [Authorize]
    public class OrderController : SiteBaseController
    {
        #region Constructor

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion Constructor

        #region AddProductToOrder

        [HttpPost]
        [Route("add-order")]
        public async Task<ActionResult> AddProductToOrder(int productId, int count,
            CancellationToken cancellationToken)
        {
            if (!User.Identity.IsAuthenticated)
                throw new BadHttpRequestException("لطف وارد سایت شوید");

            var userId = int.Parse(User.Identity.GetUserId());

            await _orderService.AddProductToOrderAsync(userId, productId, count, cancellationToken);

            var productOrderDetails = await _orderService.GetUserBasketDetails(userId, cancellationToken);

            return Ok(productOrderDetails);
        }

        #endregion AddProductToOrder

        #region UserBasketDetails

        [HttpGet]
        [Route("Get-Order-Details")]
        public async Task<ActionResult> GetUserBasketDetails(CancellationToken cancellationToken)
        {
            if (!User.Identity.IsAuthenticated)
                throw new BadHttpRequestException("لطف وارد سایت شوید");

            var userId = int.Parse(User.Identity.GetUserId());
            var details = await _orderService.GetUserBasketDetails(userId, cancellationToken);
            return Ok(details);
        }

        #endregion UserBasketDetails

        #region RemoveOrderDetail

        [HttpGet]
        [Route("remove-order-detail/{detailId}")]
        public async Task<ActionResult> RemoveOrderDetail(int detailId, CancellationToken cancellationToken)
        {
            if (!User.Identity.IsAuthenticated)
                throw new BadHttpRequestException("لطف وارد سایت شوید");

            var userId = int.Parse(User.Identity.GetUserId());

            await _orderService.RemoveOrderDetails(userId, detailId, cancellationToken);

            var OrderDetails = await _orderService.GetUserBasketDetails(userId, cancellationToken);

            return Ok(OrderDetails);
        }

        #endregion RemoveOrderDetail
    }
}