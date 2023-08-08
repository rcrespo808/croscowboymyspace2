using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Resources;
using POS.Domain;
using POS.Helper;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POS.Repository
{
    public class InventoryRepository
        : GenericRepository<Inventory, POSDbContext>, IInventoryRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IInventoryHistoryRepository _inventoryHistoryRepository;
        private readonly IMapper _mapper;

        public InventoryRepository(IUnitOfWork<POSDbContext> uow,
            IPropertyMappingService propertyMappingService,
            IInventoryHistoryRepository inventoryHistoryRepository,
            IMapper mapper)
          : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _inventoryHistoryRepository = inventoryHistoryRepository;
            _mapper = mapper;
        }

        public async Task AddInventory(InventoryDto inventory)
        {
            var existingInventory = await All.Where(x => x.ProductId == inventory.ProductId).FirstOrDefaultAsync();
            if (existingInventory == null)
            {
                _inventoryHistoryRepository.Add(new InventoryHistory
                {
                    ProductId = inventory.ProductId,
                    InventorySource = inventory.InventorySource,
                    Stock = inventory.InventorySource == InventorySourceEnum.SalesOrder ? (-1) * inventory.Stock : inventory.Stock,
                    PricePerUnit = inventory.PricePerUnit,
                    PreviousTotalStock = 0,
                    SalesOrderId = inventory.SalesOrderId,
                    PurchaseOrderId = inventory.PurchaseOrderId
                });

                var inventoryToAdd = new Inventory
                {
                    ProductId = inventory.ProductId,
                };

                if (inventory.InventorySource == InventorySourceEnum.PurchaseOrder || inventory.InventorySource == InventorySourceEnum.Direct)
                {
                    inventoryToAdd.Stock = inventory.Stock;
                    inventoryToAdd.AveragePurchasePrice = inventory.PricePerUnit;
                }
                else
                {
                    inventoryToAdd.Stock = (-1) * inventory.Stock;
                    inventoryToAdd.AverageSalesPrice = inventory.PricePerUnit;
                }
                Add(inventoryToAdd);
            }
            else
            {
                if (inventory.InventorySource == InventorySourceEnum.DeletePurchaseOrder)
                {
                    var existingPurchaseInventoryHistory = await _inventoryHistoryRepository.All
                        .FirstOrDefaultAsync(c => inventory.ProductId == c.ProductId && inventory.PurchaseOrderId.HasValue && c.PurchaseOrderId == inventory.PurchaseOrderId);
                    if (existingPurchaseInventoryHistory != null)
                    {
                        var purchaseOrderTotalStock = _inventoryHistoryRepository.All.Where(c => c.ProductId == inventory.ProductId
                        && (c.InventorySource == InventorySourceEnum.PurchaseOrder || c.InventorySource == InventorySourceEnum.Direct
                        || c.InventorySource == InventorySourceEnum.PurchaseOrderReturn)).Sum(c => c.Stock);

                        if (purchaseOrderTotalStock - inventory.Stock == 0)
                        {
                            existingInventory.AveragePurchasePrice = 0;
                        }
                        else
                        {
                            existingInventory.AveragePurchasePrice =
                                ((existingInventory.AveragePurchasePrice * purchaseOrderTotalStock) - (inventory.PricePerUnit * inventory.Stock)) / (purchaseOrderTotalStock - inventory.Stock);
                        }
                        existingInventory.Stock -= inventory.Stock;
                        _inventoryHistoryRepository.Remove(existingPurchaseInventoryHistory);
                    }
                }
                else if (inventory.InventorySource == InventorySourceEnum.DeleteSalesOrder)
                {
                    var existingPurchaseInventoryHistory = await _inventoryHistoryRepository.All
                        .FirstOrDefaultAsync(c => inventory.ProductId == c.ProductId && inventory.SalesOrderId.HasValue && c.SalesOrderId == inventory.SalesOrderId);
                    if (existingPurchaseInventoryHistory != null)
                    {
                        var salesOrderTotalStock = _inventoryHistoryRepository.All
                            .Where(c => (c.InventorySource == InventorySourceEnum.SalesOrder || c.InventorySource == InventorySourceEnum.SalesOrderReturn)
                            && c.ProductId == inventory.ProductId).Sum(c => c.Stock);

                        if (salesOrderTotalStock + inventory.Stock == 0)
                        {
                            existingInventory.AverageSalesPrice = 0;
                        }
                        else
                        {
                            existingInventory.AverageSalesPrice =
                                ((-1) * (existingInventory.AverageSalesPrice * salesOrderTotalStock) - (inventory.PricePerUnit * inventory.Stock)) / ((-1) * salesOrderTotalStock - inventory.Stock);
                        }
                        existingInventory.Stock += inventory.Stock;
                        _inventoryHistoryRepository.Remove(existingPurchaseInventoryHistory);
                    }
                }
                else if (inventory.InventorySource == InventorySourceEnum.PurchaseOrder || inventory.InventorySource == InventorySourceEnum.Direct)
                {
                    var existingPurchaseInventoryHistory = await _inventoryHistoryRepository.All.FirstOrDefaultAsync(c => inventory.ProductId == c.ProductId && inventory.PurchaseOrderId.HasValue && c.PurchaseOrderId == inventory.PurchaseOrderId);
                    var purchaseOrderTotalStock = _inventoryHistoryRepository.All.Where(c => c.ProductId == inventory.ProductId
                    && (c.InventorySource == InventorySourceEnum.PurchaseOrder || c.InventorySource == InventorySourceEnum.Direct || c.InventorySource == InventorySourceEnum.PurchaseOrderReturn)).Sum(c => c.Stock);
                    if (existingPurchaseInventoryHistory != null)
                    {
                        if (existingPurchaseInventoryHistory.PricePerUnit != inventory.PricePerUnit)
                        {
                            var stock = purchaseOrderTotalStock - existingPurchaseInventoryHistory.Stock + inventory.Stock;
                            existingInventory.AveragePurchasePrice =
                                Math.Abs((existingInventory.AveragePurchasePrice * purchaseOrderTotalStock - existingPurchaseInventoryHistory.PricePerUnit * existingPurchaseInventoryHistory.Stock + inventory.PricePerUnit * inventory.Stock)
                                / (stock == 0 ? Math.Abs(purchaseOrderTotalStock) : stock));
                            existingPurchaseInventoryHistory.PricePerUnit = inventory.PricePerUnit;
                        }

                        if (existingPurchaseInventoryHistory.Stock != inventory.Stock)
                        {
                            existingInventory.Stock = existingInventory.Stock - existingPurchaseInventoryHistory.Stock + inventory.Stock;
                            existingPurchaseInventoryHistory.Stock = inventory.Stock;
                        }
                        _inventoryHistoryRepository.Update(existingPurchaseInventoryHistory);
                    }
                    else
                    {
                        _inventoryHistoryRepository.Add(new InventoryHistory
                        {
                            ProductId = inventory.ProductId,
                            InventorySource = inventory.InventorySource,
                            Stock = inventory.Stock,
                            PricePerUnit = inventory.PricePerUnit,
                            PreviousTotalStock = existingInventory.Stock,
                            SalesOrderId = inventory.SalesOrderId,
                            PurchaseOrderId = inventory.PurchaseOrderId
                        });
                        existingInventory.AveragePurchasePrice =
                     (existingInventory.AveragePurchasePrice * purchaseOrderTotalStock + inventory.PricePerUnit * inventory.Stock) / (purchaseOrderTotalStock + inventory.Stock);
                        existingInventory.Stock += inventory.Stock;
                    }
                }
                else if (inventory.InventorySource == InventorySourceEnum.PurchaseOrderReturn)
                {
                    existingInventory.Stock = existingInventory.Stock - inventory.Stock;
                    _inventoryHistoryRepository.Add(new InventoryHistory
                    {
                        ProductId = inventory.ProductId,
                        InventorySource = inventory.InventorySource,
                        Stock = (-1) * inventory.Stock,
                        PricePerUnit = inventory.PricePerUnit,
                        PreviousTotalStock = existingInventory.Stock,
                        SalesOrderId = inventory.SalesOrderId,
                        PurchaseOrderId = inventory.PurchaseOrderId
                    });
                }
                else if (inventory.InventorySource == InventorySourceEnum.SalesOrderReturn)
                {
                    existingInventory.Stock = existingInventory.Stock + inventory.Stock;
                    _inventoryHistoryRepository.Add(new InventoryHistory
                    {
                        ProductId = inventory.ProductId,
                        InventorySource = inventory.InventorySource,
                        Stock = inventory.Stock,
                        PricePerUnit = inventory.PricePerUnit,
                        PreviousTotalStock = existingInventory.Stock,
                        SalesOrderId = inventory.SalesOrderId,
                        PurchaseOrderId = inventory.PurchaseOrderId
                    });
                }
                else
                {
                    var existingSalesInventoryHistory = await _inventoryHistoryRepository.All.FirstOrDefaultAsync(c => inventory.ProductId == c.ProductId && inventory.SalesOrderId.HasValue && c.SalesOrderId == inventory.SalesOrderId);
                    var salesOrderTotalStock = _inventoryHistoryRepository.All.Where(c => (c.InventorySource == InventorySourceEnum.SalesOrder || c.InventorySource == InventorySourceEnum.SalesOrderReturn) && c.ProductId == inventory.ProductId).Sum(c => c.Stock);
                    if (existingSalesInventoryHistory != null)
                    {
                        var stock = salesOrderTotalStock - existingSalesInventoryHistory.Stock + inventory.Stock;
                        if (existingSalesInventoryHistory.PricePerUnit != inventory.PricePerUnit)
                        {
                            existingInventory.AverageSalesPrice =
                            Math.Abs((existingInventory.AverageSalesPrice * salesOrderTotalStock - ((-1) * existingSalesInventoryHistory.Stock) * existingSalesInventoryHistory.PricePerUnit + inventory.PricePerUnit * inventory.Stock)
                            / (stock == 0 ? Math.Abs(salesOrderTotalStock) : stock));
                            existingSalesInventoryHistory.PricePerUnit = inventory.PricePerUnit;
                        }

                        if (existingSalesInventoryHistory.Stock != inventory.Stock)
                        {
                            existingInventory.Stock = existingInventory.Stock + ((-1) * existingSalesInventoryHistory.Stock) - inventory.Stock;
                            existingSalesInventoryHistory.Stock = (-1) * inventory.Stock;
                        }
                        _inventoryHistoryRepository.Update(existingSalesInventoryHistory);
                    }
                    else
                    {
                        _inventoryHistoryRepository.Add(new InventoryHistory
                        {
                            ProductId = inventory.ProductId,
                            InventorySource = inventory.InventorySource,
                            Stock = (-1) * inventory.Stock,
                            PricePerUnit = inventory.PricePerUnit,
                            PreviousTotalStock = existingInventory.Stock,
                            SalesOrderId = inventory.SalesOrderId,
                            PurchaseOrderId = inventory.PurchaseOrderId
                        });
                        salesOrderTotalStock = Math.Abs(salesOrderTotalStock);
                        existingInventory.AverageSalesPrice =
                             Math.Abs((existingInventory.AverageSalesPrice * salesOrderTotalStock + inventory.PricePerUnit * inventory.Stock) / (salesOrderTotalStock + inventory.Stock));
                        existingInventory.Stock -= inventory.Stock;
                    }

                }
                Update(existingInventory);
            }
        }

        public async Task<InventoryList> GetInventories(InventoryResource inventoryResource)
        {
            var collectionBeforePaging =
                AllIncluding(c => c.Product, u => u.Product.Unit).ApplySort(inventoryResource.OrderBy,
                _propertyMappingService.GetPropertyMapping<InventoryDto, Inventory>());

            if (!string.IsNullOrWhiteSpace(inventoryResource.ProductName))
            {
                // trim & ignore casing
                var genreForWhereClause = inventoryResource.ProductName
                    .Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Product.Name, $"{encodingName}%"));
            }

            var inventoryList = new InventoryList();
            return await inventoryList.Create(collectionBeforePaging, inventoryResource.Skip, inventoryResource.PageSize);
        }
    }
}
