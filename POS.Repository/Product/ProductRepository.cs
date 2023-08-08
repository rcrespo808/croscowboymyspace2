using AutoMapper;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Resources;
using POS.Domain;
using POS.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using POS.Helper.Constants;
using POS.Repository.Helper.ListGenerator;

namespace POS.Repository
{
    public class ProductRepository
        : GenericRepository<Product, POSDbContext>, IProductRepository
    {

        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;

        public ProductRepository(IUnitOfWork<POSDbContext> uow,
            IPropertyMappingService propertyMappingService,
            IMapper mapper,
            PathHelper pathHelper)
          : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
            _pathHelper = pathHelper;
        }

        public async Task<ProductList> GetProducts(ProductResource productResource)
        {
            string servicio = Constants.Servicios.FirstOrDefault(s => s.Value == productResource.CategoryId).Key;

            IQueryable<Product> collectionBeforePaging;

            switch (servicio)
            {
                case Constants.BENEFICIO_USUARIO:
                    collectionBeforePaging = AllIncluding(cs => cs.ProductCategory, u => u.Unit);
                    break;
                case Constants.BENEFICIO_SOCIO:
                    collectionBeforePaging = AllIncluding(cs => cs.ProductCategory, u => u.Unit);
                    break;
                default:
                    collectionBeforePaging = AllIncluding(c => c.Brand, cs => cs.ProductCategory, u => u.Unit, su => su.Supplier);
                    break;
            }

            collectionBeforePaging = collectionBeforePaging.ApplySort(productResource.OrderBy, _propertyMappingService.GetPropertyMapping<ProductDto, Product>());

            if (!string.IsNullOrEmpty(productResource.Name))
            {
                // trim & ignore casing
                var genreForWhereClause = productResource.Name
                    .Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Name, $"{encodingName}%"));
            }

            if (productResource.UnitId.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.UnitId == productResource.UnitId.Value);
            }
            if (productResource.CategoryId.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.CategoryId == productResource.CategoryId.Value || a.ProductCategory.ParentId == productResource.CategoryId.Value);
            }

            if (productResource.BrandId.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.BrandId == productResource.BrandId.Value);
            }

            var responseList = new ProductList(_mapper, _pathHelper);
            switch (servicio)
            {
                case Constants.BENEFICIO_USUARIO:
                    return await responseList.CreateUserBenefits(collectionBeforePaging, productResource.Skip, productResource.PageSize);
                case Constants.BENEFICIO_SOCIO:
                    return await responseList.CreateUserBenefits(collectionBeforePaging, productResource.Skip, productResource.PageSize);
                default:
                    return await responseList.Create(collectionBeforePaging, productResource.Skip, productResource.PageSize);
            }
        }
    }
}
