using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Entities.Product;

namespace AngularEshop.Services.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        public string Name { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "نام تصویر")]
        public string ImageName { get; set; }

        [Display(Name = "موجود / به اتمام رسیده")]
        public bool IsExists { get; set; }

        [Display(Name = "ویژه")]
        public bool IsSpecial { get; set; }

        public IEnumerable<ProductGalleryDto> ProductGalleries { get; set; }


    }
}
