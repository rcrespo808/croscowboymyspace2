﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Barcode { get; set; }
        public string SkuCode { get; set; }
        public string SkuName { get; set; }
        public string Description { get; set; }
        public string ProductUrl { get; set; }
        public string QRCodeUrl { get; set; }
        public Guid UnitId { get; set; }
        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalesPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PartnerPrice { get; set; }
        
        public string UrlWhatsapp { get; set; }
        
        public string UrlWeb { get; set; }
        
        public string Phone { get; set; }

        public string TypeSale { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Mrp { get; set; }

        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public ProductCategory ProductCategory { get; set; }

        public Guid SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public Guid BrandId { get; set; }

        public Brand Brand { get; set; }

        public List<ProductTax> ProductTaxes { get; set; }

        public int? Discount { get; set; }

        public int? LimitAttendee { get; set; }

        public bool? HasLimitAttendee { get; set; }

        public string Color { get; set; }
    }
}
