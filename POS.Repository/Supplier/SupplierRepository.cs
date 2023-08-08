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

namespace POS.Repository
{
    public class SupplierRepository : GenericRepository<Supplier, POSDbContext>, ISupplierRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly PathHelper _pathHelper;
        public SupplierRepository(
            IUnitOfWork<POSDbContext> uow,
            IPropertyMappingService propertyMappingService,
             IMapper mapper,
             IPurchaseOrderRepository purchaseOrderRepository,
             PathHelper pathHelper)
            : base(uow)
        {
            _mapper = mapper;
            _propertyMappingService = propertyMappingService;
            _purchaseOrderRepository = purchaseOrderRepository;
            _pathHelper = pathHelper;
        }

        public async Task<SupplierList> GetSuppliers(SupplierResource supplierResource)
        {
            var collectionBeforePaging =
                All.ApplySort(supplierResource.OrderBy,
                _propertyMappingService.GetPropertyMapping<SupplierDto, Supplier>());

            if (supplierResource.Id != null)
            {
                // trim & ignore casing
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Id == supplierResource.Id);
            }

            if (!string.IsNullOrEmpty(supplierResource.SupplierName))
            {
                // trim & ignore casing
                var genreForWhereClause = supplierResource.SupplierName
                    .Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.SupplierName, $"{encodingName}%"));
            }

            if (supplierResource.Sector.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Sector == supplierResource.Sector.Value);
            }

            if (!string.IsNullOrEmpty(supplierResource.PhoneNo))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = supplierResource.PhoneNo
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => (a.PhoneNo != null && EF.Functions.Like(a.PhoneNo, $"%{searchQueryForWhereClause}%")));
            }
            if (!string.IsNullOrEmpty(supplierResource.Email))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = supplierResource.Email
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Email, $"{searchQueryForWhereClause}%"));
            }
            if (!string.IsNullOrEmpty(supplierResource.MainActivity))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = supplierResource.MainActivity
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.MainActivity, $"{searchQueryForWhereClause}%"));
            }
            if (!string.IsNullOrEmpty(supplierResource.SecondaryActivity))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = supplierResource.SecondaryActivity
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.SecondaryActivity, $"{searchQueryForWhereClause}%"));
            }
            if (!string.IsNullOrEmpty(supplierResource.Area))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = supplierResource.Area
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Area, $"{searchQueryForWhereClause}%"));
            }
            if (!string.IsNullOrEmpty(supplierResource.Website))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = supplierResource.Website
                    .Trim().ToLowerInvariant();

                var name = Uri.UnescapeDataString(searchQueryForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Website, $"%{encodingName}%"));
            }
            if (!string.IsNullOrEmpty(supplierResource.SearchQuery))
            {
                var searchQueryForWhereClause = supplierResource.SearchQuery
              .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a =>
                    EF.Functions.Like(a.SupplierName, $"%{searchQueryForWhereClause}%")
                    || EF.Functions.Like(a.Sector, $"{searchQueryForWhereClause}%")
                    //|| (a.PhoneNo != null && EF.Functions.Like(a.PhoneNo, $"{searchQueryForWhereClause}%"))
                    || EF.Functions.Like(a.MainActivity, $"%{searchQueryForWhereClause}%")
                    || EF.Functions.Like(a.SecondaryActivity, $"%{searchQueryForWhereClause}%")
                    || EF.Functions.Like(a.Area, $"%{searchQueryForWhereClause}%")
                    || EF.Functions.Like(a.Objeto, $"%{searchQueryForWhereClause}%")
                    || EF.Functions.Like(a.Nit, $"{searchQueryForWhereClause}%")
                    );
            }

            //if (!string.IsNullOrWhiteSpace(supplierResource.Country))
            //{
            //    collectionBeforePaging = collectionBeforePaging
            //      .Where(a => a.SupplierAddress.Country.CountryName == supplierResource.Country);
            //}

            var SupplierList = new SupplierList(_mapper, _pathHelper);
            return await SupplierList.Create(collectionBeforePaging, supplierResource.Skip, supplierResource.PageSize);
        }

        public async Task<SupplierPaymentList> GetSuppliersPayment(SupplierResource supplierResource)
        {
            var collectionBeforePaging =
                _purchaseOrderRepository
                .AllIncluding(c => c.Supplier)
                .ApplySort(supplierResource.OrderBy,
                _propertyMappingService.GetPropertyMapping<PurchaseOrderDto, PurchaseOrder>());

            if (!string.IsNullOrEmpty(supplierResource.SupplierName))
            {
                // trim & ignore casing
                var genreForWhereClause = supplierResource.SupplierName
                    .Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Supplier.SupplierName, $"{encodingName}%"));
            }

            var groupedCollection = collectionBeforePaging.GroupBy(c => c.SupplierId);

            var supplierPayments = new SupplierPaymentList();
            return await supplierPayments.Create(groupedCollection, supplierResource.Skip, supplierResource.PageSize);
        }

        public async Task<SupplierList> GetSuppliersInterest(SupplierResource supplierResource)
        {
            supplierResource.ActiveTopInterest = true;
            var result = GetSuppliers(supplierResource);
            return await result;
        }

        public async Task<SupplierList> GetSuppliersPublicity(SupplierResource supplierResource)
        {
            supplierResource.ActiveTopPublicity = true;
            var result = GetSuppliers(supplierResource);
            return await result;
        }
    }
}
