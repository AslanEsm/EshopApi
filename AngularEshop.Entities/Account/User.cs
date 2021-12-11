using AngularEshop.Entities.Common;
using AngularEshop.Entities.Orders;
using AngularEshop.Entities.Product;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AngularEshop.Entities.Account
{
    public class User : IdentityUser<int>, IEntity
    {
        #region Properties

        public string Address { get; set; }

        public DateTimeOffset? LastLoginDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        #endregion Properties

        #region Relation

        public ICollection<ProductComment> ProductComments { get; set; }

        public ICollection<Order> Orders { get; set; }

        #endregion Relation
    }
}