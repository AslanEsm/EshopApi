using AngularEshop.Entities.Common;
using System.Collections.Generic;

namespace AngularEshop.Entities.Product
{
    public class ProductCategory : BaseEntity
    {
        #region Properties

        public string Title { get; set; }
        public string UrlTitle { get; set; }
        public int? ParentId { get; set; }

        #endregion Properties

        #region Relations

        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        #endregion Relations
    }
}