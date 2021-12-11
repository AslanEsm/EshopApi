using AngularEshop.Entities.Common;

namespace AngularEshop.Entities.Orders
{
    public class OrderDetail : BaseEntity
    {
        #region Properties

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }


        #endregion Properties

        #region Relations

        public Order Order { get; set; }

        public Product.Product Product { get; set; }

        #endregion Relations
    }
}