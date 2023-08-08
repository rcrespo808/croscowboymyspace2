using POS.Helper;
using POS.Helper.Enum;
using System;

namespace POS.Data.Resources
{
    public class SupplierResource : ResourceParameters
    {
        public SupplierResource() : base("SupplierName")
        {

        }
        public Guid? Id { get; set; }
        public string SupplierName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string MainActivity { get; set; }
        public string SecondaryActivity { get; set; }
        public string Area { get; set; }
        public Sector? Sector { get; set; }

        public bool ActiveTopInterest { get; set; } = false;
        public bool ActiveTopPublicity { get; set; } = false;
    }
}
