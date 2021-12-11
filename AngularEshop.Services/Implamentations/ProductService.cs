using AngularEshop.Common.Exceptions;
using AngularEshop.Common.Utilities.paging;
using AngularEshop.Data.Contracts;
using AngularEshop.Entities.Product;
using AngularEshop.Services.DTOs;
using AngularEshop.Services.Intefaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AngularEshop.Services.Implamentations
{
    public class ProductService : IProductService
    {
        #region Constructor

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductGallery> _productGalleryRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IRepository<ProductVisit> _productVisitRepository;
        private readonly IRepository<ProductSelectedCategory> _productSelectedCategoryRepository;
        private readonly IRepository<ProductComment> _productCommentRepository;
        private readonly IMapper _mapper;
        private IConfigurationProvider _configuration;

        public ProductService(IRepository<Product> productRepository,
            IRepository<ProductGallery> productGalleryRepository,
            IRepository<ProductCategory> productCategoryRepository,
            IRepository<ProductVisit> productVisitRepository,
            IRepository<ProductSelectedCategory> productSelectedCategoryRepository,
            IRepository<ProductComment> productCommentRepository,
            IMapper mapper,
            IConfigurationProvider configuration)
        {
            _productRepository = productRepository;
            _productGalleryRepository = productGalleryRepository;
            _productCategoryRepository = productCategoryRepository;
            _productVisitRepository = productVisitRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
            _productCommentRepository = productCommentRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        #endregion Constructor

        #region Product

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _productRepository.AddAsync(product, cancellationToken);
        }

        public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _productRepository.UpdateAsync(product, cancellationToken);
        }

        public async Task<FilterProductDto> FilterProducts(FilterProductDto filter)
        {
            var productsQuery = _productRepository.TableNoTracking;

            if (filter.SortOrder != null)
            {
                switch (filter.SortOrder)
                {
                    case SortOrderResult.Date:
                        productsQuery = productsQuery.OrderBy(p => p.CreateDate);
                        break;

                    case SortOrderResult.Date_Desc:
                        productsQuery = productsQuery.OrderByDescending(p => p.CreateDate);
                        break;

                    case SortOrderResult.Price:
                        productsQuery = productsQuery.OrderBy(p => p.Price);
                        break;

                    case SortOrderResult.Price_Desc:
                        productsQuery = productsQuery.OrderByDescending(p => p.Price);
                        break;

                    case SortOrderResult.Name:
                        productsQuery = productsQuery.OrderBy(p => p.Name);
                        break;

                    case SortOrderResult.Name_Desc:
                        productsQuery = productsQuery.OrderByDescending(p => p.Name);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(filter.Title) ||
                    p.ShortDescription.Contains(filter.Title) ||
                    p.Description.Contains(filter.Title));
            }

            if (filter.StartPrice != 0)
                productsQuery = productsQuery.Where(p => p.Price >= filter.StartPrice);

            if (filter.EndPrice != 0)
                productsQuery = productsQuery.Where(p => p.Price >= filter.EndPrice);

            if (filter.Categories != null && filter.Categories.Any())
            {
                productsQuery = productsQuery.SelectMany(p =>
                        p.ProductSelectedCategories.Where(f => filter.Categories.Contains(f.ProductCategoryId)))
                    .Select(t => t.Product);
            }

            productsQuery = productsQuery.Where(p => p.Price >= filter.StartPrice && p.Price <= filter.EndPrice);

            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var products = await productsQuery.Paging(pager).ToListAsync();

            return filter.SetProduct(products).SetPaging(pager);
        }

        public async Task<ProductDto> GetProductAsync(int id, CancellationToken cancellationToken)
        {
            #region OldCode

            //var product = await _productRepository.TableNoTracking
            //    .Include(p => p.ProductGalleries)
            //    .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

            #endregion OldCode

            //better performance
            var product = await _productRepository.TableNoTracking
                .ProjectTo<ProductDto>(_configuration)
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                throw new NotFoundException("همچین کالایی وجود ندارد");

            //var dto = _mapper.Map<Product, ProductDto>(product);
            return product;
        }

        public async Task<bool> IsExistProductById(int id)
        {
            var res = await _productRepository.TableNoTracking
                .AnyAsync(p => p.Id == id);
            return res;
        }

        #endregion Product

        #region ProductCategory

        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            var productCategory = await _productCategoryRepository.TableNoTracking
                .ToListAsync();
            return productCategory;
        }

        #endregion ProductCategory

        #region ProductGallery

        public async Task<List<ProductGallery>> GetActiveProductGalleries(int id)
        {
            var productGalleries =
                await _productGalleryRepository.TableNoTracking.Where(p => p.ProductId == id )
                    .ToListAsync();

            if (productGalleries == null)
                throw new NotFoundException("هیج موردی برای نمایش یافت نشد");

            return productGalleries;
        }

        #endregion ProductGallery

        #region GetRelatedProducts

        public async Task<List<ProductDto>> GetRelatedProductsAsync(int id)
        {
            var productCategoryList = await _productSelectedCategoryRepository.TableNoTracking
                .Where(p => p.ProductId == id)
                .Select(c => c.ProductCategoryId)
                .ToListAsync();

            if (productCategoryList.Count < 2)
                throw new NotFoundException("محصولات مشابه این محصول وجود ندارند");

            var relatedProducts = await _productRepository.TableNoTracking
                .SelectMany(p =>
                    p.ProductSelectedCategories.Where(c => productCategoryList.Contains(c.ProductCategoryId)))
                .Select(t => t.Product)
                .Where(s => s.Id != id)
                .OrderByDescending(d => d.CreateDate)
                .Take(4)
                .ToListAsync();

            var productsDto = _mapper.Map<List<Product>, List<ProductDto>>(relatedProducts);

            return productsDto;
        }

        #endregion GetRelatedProducts

        #region ProductComments

        public async Task<ProductCommentDto> AddCommentToProductAsync(AddProductCommentDto comment, string userId, CancellationToken cancellationToken)
        {
            var res = await IsExistProductById(comment.ProductId);

            if (!res)
                throw new NotFoundException();

            var commentData = new ProductComment()
            {
                ProductId = comment.ProductId,
                Text = comment.Text,
                UserId = int.Parse(userId),
            };

            await _productCommentRepository.AddAsync(commentData, cancellationToken);

            return new ProductCommentDto()
            {
                Id = commentData.Id,
                CreateDate = commentData.CreateDate.ToString("yyyy/MM/dd"),
                Text = commentData.Text,
                UserId = commentData.UserId.ToString(),
                UserName = commentData.User.UserName

            };
        }

        public async Task<List<ProductCommentDto>> GetActiveProductCommentsAsync(int id)
        {
            var commnets = await _productCommentRepository.TableNoTracking
                .Include(c => c.User)
                .Where(p => p.ProductId == id && p.IsAccepted)
                .OrderByDescending(h => h.CreateDate)
                .Select(s => new ProductCommentDto()
                {
                    Id = s.Id,
                    CreateDate = s.CreateDate.ToString("yyyy/MM/dd"),
                    Text = s.Text,
                    UserId = s.UserId.ToString(),
                    UserName = s.User.UserName
                })
                .ToListAsync();

            if (commnets == null)
                throw new NotFoundException("موردی یافت نشد");

            return commnets;
        }

        #endregion ProductComments

        #region Dispose

        public void Dispose()
        {
            _productRepository?.Dispose();
            _productGalleryRepository?.Dispose();
            _productCategoryRepository?.Dispose();
            _productVisitRepository?.Dispose();
            _productSelectedCategoryRepository?.Dispose();
            _productCommentRepository?.Dispose();
        }

        #endregion Dispose
    }
}