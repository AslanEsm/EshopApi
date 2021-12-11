using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Entities.Common;

namespace AngularEshop.Entities.Product
{
    public class ProductVisit : BaseEntity
    {
        #region Properties

        public int ProductId { get; set; }
        public string UserIp { get; set; }

        #endregion

        #region Relations

        public Product Product { get; set; }

        #endregion
    }
}
