using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;

namespace POS.MediatR.Handlers
{
    public class ApprovePurchaseOrderCommandHandler
        : IRequestHandler<ApprovePurchaseOrderCommand, ServiceResponse<bool>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILogger<ApprovePurchaseOrderCommandHandler> _logger;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public ApprovePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository,
            ILogger<ApprovePurchaseOrderCommandHandler> logger,
            IUnitOfWork<POSDbContext> uow,
            IInventoryRepository inventoryRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _logger = logger;
            _uow = uow;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<ServiceResponse<bool>> Handle(ApprovePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await _purchaseOrderRepository.All.Include(c => c.PurchaseOrderItems)
                .ThenInclude(c=>c.PurchaseOrderItemTaxes)
                .ThenInclude(c=>c.Tax)
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (purchaseOrder == null)
            {
                _logger.LogError("Purchase order does not exists.");
                return ServiceResponse<bool>.Return404();
            }

            purchaseOrder.Status = Data.PurchaseOrderStatus.Not_Return;
            _purchaseOrderRepository.Update(purchaseOrder);

            var inventoriesToAdd = purchaseOrder.PurchaseOrderItems
                .GroupBy(c => c.ProductId)
                .Select(cs => new InventoryDto
                {
                    InventorySource = Data.InventorySourceEnum.PurchaseOrder,
                    ProductId = cs.Key,
                    PricePerUnit = cs.Sum(d => d.UnitPrice * d.Quantity + d.TaxValue - d.Discount) / cs.Sum(d => d.Quantity),
                    PurchaseOrderId = request.Id,
                    Stock = cs.Sum(d => d.Quantity),
                }).ToList();
            foreach (var inventory in inventoriesToAdd)
            {
                await _inventoryRepository.AddInventory(inventory);
            }

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while updating Purchase order.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
