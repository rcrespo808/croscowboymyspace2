using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data.Dto
{
    public class InventoryDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public long Stock { get; set; }
        public decimal PricePerUnit { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }
        public decimal AveragePurchasePrice { get; set; }
        public decimal AverageSalesPrice { get; set; }
        public InventorySourceEnum InventorySource { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid? SalesOrderId { get; set; }
    }
}
