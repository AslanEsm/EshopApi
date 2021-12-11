using AngularEshop.Common.Utilities.paging;
using AngularEshop.Entities.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularEshop.Services.DTOs
{
    public class FilterProductDto : BasePaging
    {
        public string Title { get; set; }
        public int StartPrice { get; set; }
        public int EndPrice { get; set; }
        public SortOrderResult? SortOrder { get; set; }
        public List<Product> Products { get; set; }

        public List<long> Categories { get; set; }

        public FilterProductDto SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.ActivePage = paging.ActivePage;

            return this;
        }

        public FilterProductDto SetProduct(List<Product> products)
        {
            this.Products = products;
            return this;
        }
    }

    public enum SortOrderResult
    {
        [Display(Name = "تاریخ")]
        Date,

        [Display(Name = "تاریخ_نزولی")]
        Date_Desc,

        [Display(Name = "قیمت")]
        Price,

        [Display(Name = "قیمت_نزولی")]
        Price_Desc,

        [Display(Name = "اسم")]
        Name,

        [Display(Name = "اسم_نزولی")]
        Name_Desc
    }
}