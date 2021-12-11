using System.ComponentModel.DataAnnotations;

namespace AngularEshop.Services.DTOs
{
    public class AddProductCommentDto
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(1000, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [Display(Name = "نظر")]
        public string Text { get; set; }

        [Display(Name = "محصول")]
        public int ProductId { get; set; }
    }
}