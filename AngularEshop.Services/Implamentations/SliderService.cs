using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngularEshop.Data.Contracts;
using AngularEshop.Data.Repositories;
using AngularEshop.Entities.Site;
using AngularEshop.Services.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace AngularEshop.Services.Implamentations
{
    public class SliderService : ISliderService
    {
        #region Constructor
        private readonly IRepository<Slider> _sliderRepository;

        public SliderService(IRepository<Slider> sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        #endregion


        #region Slider


        public async Task<List<Slider>> GetActiveSlidersAsync()
        {
            var activeSliders = await _sliderRepository.TableNoTracking.ToListAsync();
            return activeSliders;
        }

        #region slider

        public async Task<List<Slider>> GetAllSlidersAsync()
        {
            var sliders = await _sliderRepository.TableNoTracking.ToListAsync();
            return sliders;
        }

        public async Task<Slider> GetSliderByIdAsync(CancellationToken cancellationToken, int id)
        {
            var slider = await _sliderRepository.GetByIdAsync(cancellationToken, id);
            return slider;
        }

        public async Task AddSliderAsync(Slider slider, CancellationToken cancellationToken)
        {
            await _sliderRepository.AddAsync(slider, cancellationToken);
        }

        public async Task UpdateSliderAsync(Slider slider, CancellationToken cancellationToken)
        {
            await _sliderRepository.UpdateAsync(slider, cancellationToken);
        }



        #endregion



        #endregion

        #region Dispose

        public void Dispose()
        {
            _sliderRepository?.Dispose();
        }


        #endregion



    }
}
