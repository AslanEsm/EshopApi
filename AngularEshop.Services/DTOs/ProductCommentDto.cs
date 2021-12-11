using System.ComponentModel.DataAnnotations;

namespace AngularEshop.Services.DTOs
{
    public class ProductCommentDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [Display(Name = "نظر")]
        public string Text { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public string CreateDate { get; set; }
    }


}