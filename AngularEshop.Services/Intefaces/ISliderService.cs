using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AngularEshop.Data.Contracts;
using AngularEshop.Entities.Site;

namespace AngularEshop.Services.Intefaces
{
    public interface ISliderService : IDisposable
    {
        Task<List<Slider>> GetActiveSlidersAsync();
        Task<List<Slider>> GetAllSlidersAsync();
        Task<Slider> GetSliderByIdAsync(CancellationToken cancellationToken, int id);
        Task AddSliderAsync(Slider slider, CancellationToken cancellationToken);
        Task UpdateSliderAsync(Slider slider, CancellationToken cancellationToken);
    }
}
