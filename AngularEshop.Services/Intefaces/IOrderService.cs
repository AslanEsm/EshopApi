using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AngularEshop.Entities.Orders;
using AngularEshop.Services.DTOs;

namespace AngularEshop.Services.Intefaces
{
    public interface IOrderService : IDisposable
    {
        #region order

        Task<Order> CreateUserOrderAsync(int userId, CancellationToken cancellationToken);
        Task<Order> GetUserOpenOrderAsync(int userId, CancellationToken cancellationToken);

        #endregion

        #region orderDetail

        Task AddProductToOrderAsync(int userId, int productId, int count, CancellationToken cancellationToken);
        Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId);
        Task<OrderDetail> GetOrderDetailByIdAsync(int userId, int detailId, CancellationToken cancellationToken);
        Task<List<OrderBasketDetail>> GetUserBasketDetails(int userId, CancellationToken cancellationToken);
        Task RemoveOrderDetails(int userId, int detailId, CancellationToken cancellationToken);

        #endregion
    }
}