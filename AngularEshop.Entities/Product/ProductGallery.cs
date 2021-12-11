using System.ComponentModel.DataAnnotations;
using AngularEshop.Entities.Common;

namespace AngularEshop.Entities.Product
{
    public class ProductGallery : BaseEntity
    {
        #region Properties

        public int ProductId { get; set; }
        public string ImageName { get; set; }



        #endregion

        #region Relations

        public Product Product { get; set; }

        #endregion
    }
}
