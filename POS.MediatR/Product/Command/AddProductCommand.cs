using MediatR;
using POS.Data.Dto;
using POS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.MediatR.Product.Command
{
    public class AddProductCommand : IRequest<ServiceResponse<ProductDto>>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Barcode { get; set; }

        public string SkuCode { get; set; }

        public string SkuName { get; set; }

        public string Description { get; set; }

        public string ProductUrlData { get; set; }

        public string QRCodeUrlData { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? BrandId { get; set; } = Guid.Empty;

        public decimal? PurchasePrice { get; set; }

        public decimal? SalesPrice { get; set; }

        public decimal? PartnerPrice { get; set; }

        public string? UrlWhatsapp { get; set; }

        public string? UrlWeb { get; set; }

        public string? Phone { get; set; }

        public string? TypeSale { get; set; }

        public decimal? Mrp { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? SupplierId { get; set; }

        public List<ProductTaxDto> ProductTaxes { get; set; }

        public int? Discount { get; set; }

        public int? LimitAttendee { get; set; }

        public bool? HasLimitAttendee { get; set; }

        public string? Color { get; set; }
    }
}
