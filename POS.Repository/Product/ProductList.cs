using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Data.Dto;
using POS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Repository
{
    public class ProductList : List<ProductDto>
    {
        public IMapper _mapper { get; set; }
        public PathHelper _pathHelper { get; set; }
        public ProductList(IMapper mapper, PathHelper pathHelper)
        {
            _mapper = mapper;
            _pathHelper = pathHelper;
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public ProductList(List<ProductDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<ProductList> CreateUserBenefits(IQueryable<Product> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await source
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .Select(e => new ProductDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Code = e.Code,
                    Barcode = e.Barcode,
                    SkuCode = e.SkuCode,
                    SkuName = e.SkuName,
                    Description = e.Description,
                    ProductUrl = e.ProductUrl,
                    QRCodeUrl = e.QRCodeUrl,
                    UnitId = e.UnitId,
                    PurchasePrice = e.PurchasePrice,
                    SalesPrice = e.SalesPrice,
                    PartnerPrice = e.PartnerPrice,
                    UrlWhatsapp = e.UrlWhatsapp,
                    UrlWeb = e.UrlWeb,
                    Phone = e.Phone,
                    TypeSale = e.TypeSale,
                    Mrp = e.Mrp,
                    CategoryId = e.CategoryId,
                    CategoryName = e.ProductCategory.Name,
                    UnitName = e.Unit.Name,
                    CreatedDate = e.CreatedDate,
                    Discount = e.Discount,
                    LimitAttendee = e.LimitAttendee,
                    HasLimitAttendee = e.HasLimitAttendee,
                    Color = e.Color,
                })
                .ToListAsync();
            var dtoPageList = new ProductList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<ProductList> Create(IQueryable<Product> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new ProductList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Product> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<ProductDto>> GetDtos(IQueryable<Product> source, int skip, int pageSize)
        {
            var entities = await source
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return entities;
        }
    }
}
