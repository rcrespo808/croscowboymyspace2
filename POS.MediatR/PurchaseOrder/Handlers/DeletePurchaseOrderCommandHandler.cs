using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;

namespace POS.MediatR.Handlers
{

    public class DeletePurchaseOrderCommandHandler
       : IRequestHandler<DeletePurchaseOrderCommand, ServiceResponse<bool>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ILogger<DeletePurchaseOrderCommandHandler> _logger;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IInventoryRepository _inventoryRepository;

        public DeletePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository,
            ILogger<DeletePurchaseOrderCommandHandler> logger,
            IUnitOfWork<POSDbContext> uow,
            IInventoryRepository inventoryRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _logger = logger;
            _uow = uow;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<ServiceResponse<bool>> Handle(DeletePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await _purchaseOrderRepository.AllIncluding(c => c.PurchaseOrderItems).FirstOrDefaultAsync(c => c.Id == request.Id);

            if (purchaseOrder == null)
            {
                _logger.LogError("Purchase order does not exists.");
                return ServiceResponse<bool>.Return404();
            }

            purchaseOrder.IsDeleted = true;
            _purchaseOrderRepository.Update(purchaseOrder);

            var inventoriesToDelete = purchaseOrder.PurchaseOrderItems
                    .GroupBy(c => c.ProductId)
                    .Select(cs => new InventoryDto
                    {
                        InventorySource = Data.InventorySourceEnum.DeletePurchaseOrder,
                        ProductId = cs.Key,
                        PricePerUnit = cs.Sum(d => d.UnitPrice * d.Quantity + d.TaxValue - d.Discount) / cs.Sum(d => d.Quantity),
                        PurchaseOrderId = purchaseOrder.Id,
                        Stock = cs.Sum(d => d.Quantity),
                    }).ToList();
            foreach (var inventory in inventoriesToDelete)
            {
                await _inventoryRepository.AddInventory(inventory);
            }

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while deleting Purchase order.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
