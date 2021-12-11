using System.Linq;
using AngularEshop.Entities.Product;
using AngularEshop.Services.DTOs;
using AutoMapper;

namespace AngularEshop.Services.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductGalleries,
                    o => o.MapFrom(s => s.ProductGalleries.Select(p => new ProductGalleryDto
                    {
                        Id = p.Id,
                        ImageName = p.ImageName
                    }).ToList()));

        }
    }
}