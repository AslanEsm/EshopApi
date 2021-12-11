using AngularEshop.Entities.Account;
using AngularEshop.Entities.Common;
using System;
using System.Collections.Generic;

namespace AngularEshop.Entities.Orders
{
    public class Order : BaseEntity
    {
        #region Properties

        public int UserId { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PaymentDate { get; set; }

        #endregion Properties

        #region Relations

        public User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion Relations
    }
}