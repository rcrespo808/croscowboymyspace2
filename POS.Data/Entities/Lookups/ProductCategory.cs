﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data
{
    public class ProductCategory : BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        
        public string Description { get; set; }

        public string ImageUrl { get; set; }
        
        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public ProductCategory ParentCategory { get; set; }
    }
}
