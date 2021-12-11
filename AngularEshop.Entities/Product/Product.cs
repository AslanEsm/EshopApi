using AngularEshop.Entities.Common;
using AngularEshop.Entities.Orders;
using System.Collections.Generic;

namespace AngularEshop.Entities.Product
{
    public class Product : BaseEntity
    {
        #region Property

        public string Name { get; set; }

        public int Price { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public bool IsExists { get; set; }

        public bool IsSpecial { get; set; }

        #endregion Property

        #region Relations

        public ICollection<ProductGallery> ProductGalleries { get; set; }
        public ICollection<ProductVisit> ProductVisits { get; set; }
        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion Relations
    }
}