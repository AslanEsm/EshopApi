using AngularEshop.Common.Exceptions;
using AngularEshop.Common.Utilities;
using AngularEshop.Entities.Product;
using AngularEshop.Services.DTOs;
using AngularEshop.Services.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Filters;

namespace AngularEshop.WebApi.Controllers
{
    [ApiResultFilter]
    public class ProductController : SiteBaseController
    {
        #region Constructor

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion Constructor

        #region Products

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductDto filter)
        {
            var products = await _productService.FilterProducts(filter);
            return Ok(products);
        }

        #endregion Products

        #region ProductCategory

        [HttpGet]
        [Route("GetProductCategories")]
        public async Task<ActionResult<ProductCategory>> GetProductCategories()
        {
            var productCategories = await _productService.GetAllActiveProductCategories();
            return Ok(productCategories);
        }

        #endregion ProductCategory

        #region GetProduct

        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductAsync(id, cancellationToken);
            return Ok(product);
        }

        #endregion GetProduct

        #region GetRelatedProducts

        [HttpGet]
        [Route("GetRelatedProducts")]
        public async Task<ActionResult<List<ProductDto>>> GetRelatedProducts(int id)
        {
            var relatedProducts = await _productService.GetRelatedProductsAsync(id);

            return Ok(relatedProducts);
        }

        #endregion GetRelatedProducts

        #region ProductComments

        [HttpGet]
        [Route("GetComments/{id}")]
        public async Task<ActionResult<List<ProductComment>>> GetComments(int id)
        {
            var comments = await _productService.GetActiveProductCommentsAsync(id);

            return Ok(comments);
        }

        [HttpPost]
        [Route("add-product-comment")]
        public async Task<IActionResult> AddComment([FromBody] AddProductCommentDto comment, CancellationToken cancellationToken)
        {
            if (!User.Identity.IsAuthenticated)
                throw new BadHttpRequestException("لطف وارد سایت شوید");

            var userId = User.Identity.GetUserId();

            var res = await _productService.AddCommentToProductAsync(comment, userId, cancellationToken);

            return Ok(res);
        }

        #endregion ProductComments
    }
}