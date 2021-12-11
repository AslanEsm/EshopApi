using AngularEshop.Entities.Account;
using AngularEshop.Entities.Common;

namespace AngularEshop.Entities.Product
{
    public class ProductComment : BaseEntity
    {
        #region Property

        public string Text { get; set; }

        public bool IsAccepted { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        #endregion Property

        #region Relations

        public Product Product { get; set; }

        public User User { get; set; }

        #endregion Relations

        #region Constructor

        public ProductComment()
        {
            IsAccepted = false;
        }

        #endregion Constructor
    }
}