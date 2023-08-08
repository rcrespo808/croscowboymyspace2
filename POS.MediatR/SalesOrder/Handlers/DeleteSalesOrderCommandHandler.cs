using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class DeleteSalesOrderCommandHandler
          : IRequestHandler<DeleteSalesOrderCommand, ServiceResponse<bool>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly ILogger<DeleteSalesOrderCommandHandler> _logger;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IInventoryRepository _inventoryRepository;

        public DeleteSalesOrderCommandHandler(ISalesOrderRepository salesOrderRepository,
            ILogger<DeleteSalesOrderCommandHandler> logger,
            IUnitOfWork<POSDbContext> uow,
            IInventoryRepository inventoryRepository)
        {
            _salesOrderRepository = salesOrderRepository;
            _logger = logger;
            _uow = uow;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await _salesOrderRepository.AllIncluding(c => c.SalesOrderItems).FirstOrDefaultAsync(c => c.Id == request.Id);

            if (salesOrder == null)
            {
                _logger.LogError("Sales order does not exists.");
                return ServiceResponse<bool>.Return404();
            }

            _salesOrderRepository.Delete(salesOrder);

            var inventoriesToDelete = salesOrder.SalesOrderItems
                .GroupBy(c => c.ProductId)
                .Select(cs => new InventoryDto
                {
                    InventorySource = Data.InventorySourceEnum.DeleteSalesOrder,
                    ProductId = cs.Key,
                    PricePerUnit = cs.Sum(d => d.UnitPrice * d.Quantity + d.TaxValue - d.Discount) / cs.Sum(d => d.Quantity),
                    SalesOrderId = salesOrder.Id,
                    Stock = cs.Sum(d => d.Quantity),
                }).ToList();

            foreach (var inventory in inventoriesToDelete)
            {
                await _inventoryRepository.AddInventory(inventory);
            }

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while deleting Sales order.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }

}
