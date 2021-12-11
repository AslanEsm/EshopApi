using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Entities.Common;

namespace AngularEshop.Entities.Site
{
    public class Slider : BaseEntity
    {
        #region Properties
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "لینک")]
        public string Link { get; set; }

        #endregion
    }
}
