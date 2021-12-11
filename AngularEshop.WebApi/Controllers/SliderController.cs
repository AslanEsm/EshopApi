using AngularEshop.Entities.Site;
using AngularEshop.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFramework.Filters;

namespace AngularEshop.WebApi.Controllers
{
    [ApiResultFilter]
    public class SliderController : SiteBaseController
    {
        #region SliderService

        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        #endregion SliderService

        #region AllActiveSliders

        [HttpGet("GetActiveSliders")]
        public async Task<ActionResult<List<Slider>>> GetActiveSliders()
        {
            var activeSliders = await _sliderService.GetActiveSlidersAsync();
            return activeSliders;
        }

        #endregion AllActiveSliders
    }
}