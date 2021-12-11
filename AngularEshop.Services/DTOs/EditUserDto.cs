using System.ComponentModel.DataAnnotations;

namespace AngularEshop.Services.DTOs
{
    public class EditUserDto
    {
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; }
    }
}