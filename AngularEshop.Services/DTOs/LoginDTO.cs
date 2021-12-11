using System.ComponentModel.DataAnnotations;

namespace AngularEshop.Services.DTOs
{
    public class LoginDTO
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string PasswordHash { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public enum LoginResult
    {
        [Display(Name = "لاگین با موفقیت انجام شد")]
        Success,

        [Display(Name = "اکانت قفل است")]
        IsLockedOut,

        [Display(Name = "این اکانت اجازه دسترسی به سایت را ندارد")]
        IsNotAllowed,

        [Display(Name = "رمز ویا نام کاربری اشتباه است")]
        InCorrectData,

        [Display(Name = "این اکانت فعال نیست")]
        NotActivated,

        [Display(Name = "تایید دو مرحله انجام نشده است")]
        RequiresTwoFactor,

        [Display(Name = "کاربر مورد نظر ادمین نیست,شما اجازه دسترسی ندارید")]
        NotAdmin
    }
}