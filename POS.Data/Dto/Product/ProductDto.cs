using System;
using System.Collections.Generic;

namespace POS.Data.Dto
{
    public class ProductDto
    {
        public Guid? Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public string Barcode { get; set; }
        
        public string SkuCode { get; set; }
        
        public string SkuName { get; set; }
        
        public string Description { get; set; }
        
        public string ProductUrl { get; set; }
        
        public string QRCodeUrl { get; set; }
        
        public Guid UnitId { get; set; }
        
        public decimal? PurchasePrice { get; set; }
        
        public decimal? SalesPrice { get; set; }

        public decimal? PartnerPrice { get; set; }

        public string UrlWhatsapp { get; set; }

        public string UrlWeb { get; set; }

        public string Phone { get; set; }
        
        public string TypeSale { get; set; }
        
        public decimal? Mrp { get; set; }
        
        public Guid CategoryId { get; set; }
        
        public string CategoryName { get; set; }
        
        public string UnitName { get; set; }
        
        public Guid BrandId { get; set; }
        
        public string BrandName { get; set; }
        
        public UnitDto Unit { get; set; }
       
        public Guid SupplierId { get; set; }
        
        public Supplier Supplier { get; set; }
        
        public List<ProductTaxDto> ProductTaxes { get; set; }
        
        public DateTime? CreatedDate { get; set; }

        public int? Discount { get; set; }

        public int? LimitAttendee { get; set; }

        public bool? HasLimitAttendee { get; set; }

        public string Color { get; set; }
    }
}
