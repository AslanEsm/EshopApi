using AngularEshop.Common.Exceptions;
using AngularEshop.Data.Contracts;
using AngularEshop.Entities.Orders;
using AngularEshop.Services.Intefaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngularEshop.Common;
using AngularEshop.Services.DTOs;
using Microsoft.Extensions.Options;


namespace AngularEshop.Services.Implamentations
{
    public class OrderService : IOrderService
    {
        #region Constructor

        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly SiteSettings _siteSetting;


        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<OrderDetail> orderDetailRepository,
            IUserService userService,
            IProductService productService,
            IOptionsSnapshot<SiteSettings> settings
        )

        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _userService = userService;
            _productService = productService;
            _siteSetting = settings.Value;
        }

        #endregion Constructor

        #region Order

        public async Task<Order> CreateUserOrderAsync(int userId, CancellationToken cancellationToken)
        {
            var order = new Order()
            {
                UserId = userId
            };

            await _orderRepository.AddAsync(order, cancellationToken);

            return order;
        }

        public async Task<Order> GetUserOpenOrderAsync(int userId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.TableNoTracking
                .Include(c => c.OrderDetails)
                .ThenInclude(p => p.Product)
                .SingleOrDefaultAsync(o => o.UserId == userId && !o.IsPay,
                    cancellationToken: cancellationToken);

            if (order != null)
                order = await CreateUserOrderAsync(userId, cancellationToken);

            return order;
        }

        #endregion Order

        #region OrderDetail

        public async Task AddProductToOrderAsync(int userId, int productId, int count,
            CancellationToken cancellationToken)
        {
            var isUserExist = await _userService.IsUserExistById(userId);
            if (!isUserExist)
                throw new NotFoundException("لطفا لاگین کنید");

            var isProductExist = await _productService.IsExistProductById(productId);
            if (!isProductExist)
                throw new NotFoundException("محصول مورد نظر برای اضافه کردن به سبد خرید یافت نشد");

            var product = await _productService.GetProductAsync(productId, cancellationToken);
            var order = await GetUserOpenOrderAsync(userId, cancellationToken);

            if (count < 1)
                count = 1;

            var details = await GetOrderDetailsAsync(order.Id);
            var ExistDetail = details.SingleOrDefault(p => p.ProductId == productId);

            if (ExistDetail != null)
            {
                ExistDetail.Count += count;
                await _orderDetailRepository.UpdateAsync(ExistDetail, cancellationToken);
            }
            else
            {
                var orderDetail = new OrderDetail()
                {
                    OrderId = order.Id,
                    ProductId = productId,
                    Count = count,
                    Price = product.Price
                };
                await _orderDetailRepository.AddAsync(orderDetail, cancellationToken);
            }
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId)
        {
            var orderDetails = await _orderDetailRepository.TableNoTracking
                .Where(o => o.OrderId == orderId)
                .ToListAsync();

            return orderDetails;
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int userId, int detailId,
            CancellationToken cancellationToken)
        {
            var openOrder = await GetUserOpenOrderAsync(userId, cancellationToken);

            if (string.IsNullOrEmpty(openOrder.ToString()))
                throw new NotFoundException("there is not any open order");

            var detail = openOrder.OrderDetails.SingleOrDefault(s => s.Id == detailId);

            if (detail == null)
                throw new NotFoundException("there is not any order detail");

            return detail;
        }

        public async Task<List<OrderBasketDetail>> GetUserBasketDetails(int userId, CancellationToken cancellationToken)
        {
            var openOrder = await GetUserOpenOrderAsync(userId, cancellationToken);

            if (openOrder == null)
                throw new NotFoundException("هیچ اردری برای نمایش وجود ندارد");

            var orderBasketDetails = openOrder.OrderDetails
                .Select(o => new OrderBasketDetail()
                {
                    OrderDetailId = o.Id,
                    Count = o.Count,
                    Price = o.Product.Price,
                    Title = o.Product.Name,
                    ImageName = _siteSetting.PathTools.ProductImagePath +
                                _siteSetting.PathTools.ProductImagePath +
                                o.Product.ImageName
                }).ToList();

            return orderBasketDetails;
        }

        public async Task RemoveOrderDetails(int userId, int detailId, CancellationToken cancellationToken)
        {
            var detail = await GetOrderDetailByIdAsync(userId, detailId, cancellationToken);

            await _orderDetailRepository.DeleteAsync(detail, cancellationToken);
        }

        #endregion OrderDetail

        #region Dispose

        public void Dispose()
        {
            _orderRepository?.Dispose();
            _orderDetailRepository?.Dispose();
        }

        #endregion Dispose
    }
}