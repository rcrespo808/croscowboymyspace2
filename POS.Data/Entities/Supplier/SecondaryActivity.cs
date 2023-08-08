using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data
{
    public class SecondaryActivity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
