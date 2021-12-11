using AngularEshop.Entities.Product;
using AngularEshop.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AngularEshop.Services.Intefaces
{
    public interface IProductService : IDisposable
    {
        #region Product

        Task AddProductAsync(Product product, CancellationToken cancellationToken);

        Task UpdateProductAsync(Product product, CancellationToken cancellationToken);

        Task<FilterProductDto> FilterProducts(FilterProductDto filter);

        Task<ProductDto> GetProductAsync(int id, CancellationToken cancellationToken);

        Task<bool> IsExistProductById(int id);

        #endregion Product

        #region ProductCategory

        Task<List<ProductCategory>> GetAllActiveProductCategories();

        #endregion ProductCategory

        #region ProductGallery

        Task<List<ProductGallery>> GetActiveProductGalleries(int id);

        #endregion ProductGallery

        #region GetRelatedProducts

        Task<List<ProductDto>> GetRelatedProductsAsync(int id);

        #endregion GetRelatedProducts

        #region ProductComments

        Task<ProductCommentDto> AddCommentToProductAsync(AddProductCommentDto comment, string userId, CancellationToken cancellationToken);

        Task<List<ProductCommentDto>> GetActiveProductCommentsAsync(int id);

        #endregion ProductComments
    }
}