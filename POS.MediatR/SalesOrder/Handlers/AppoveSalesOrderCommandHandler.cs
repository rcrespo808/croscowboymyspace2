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
using POS.MediatR.SalesOrder.Commands;

namespace POS.MediatR.SalesOrder.Handlers
{
    public class AppoveSalesOrderCommandHandler
      : IRequestHandler<AppoveSalesOrderCommand, ServiceResponse<bool>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILogger<AppoveSalesOrderCommandHandler> _logger;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AppoveSalesOrderCommandHandler(ISalesOrderRepository salesOrderRepository,
            ILogger<AppoveSalesOrderCommandHandler> logger,
            IUnitOfWork<POSDbContext> uow,
            IInventoryRepository inventoryRepository)
        {
            _salesOrderRepository = salesOrderRepository;
            _logger = logger;
            _uow = uow;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<ServiceResponse<bool>> Handle(AppoveSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await _salesOrderRepository.AllIncluding(c => c.SalesOrderItems).FirstOrDefaultAsync(c => c.Id == request.Id);
            if (salesOrder == null)
            {
                _logger.LogError("Sales Order does not exists.");
                return ServiceResponse<bool>.Return404();
            }

            salesOrder.Status = Data.SalesOrderStatus.Not_Return;
            _salesOrderRepository.Update(salesOrder);

            var inventoriesToAdd = salesOrder.SalesOrderItems
                .GroupBy(c => c.ProductId)
                .Select(cs => new InventoryDto
                {
                    InventorySource = Data.InventorySourceEnum.SalesOrder,
                    ProductId = cs.Key,
                    PricePerUnit = cs.Sum(d => d.UnitPrice * d.Quantity + d.TaxValue - d.Discount) / cs.Sum(d => d.Quantity),
                    SalesOrderId = request.Id,
                    Stock = cs.Sum(d => d.Quantity),
                }).ToList();

            foreach (var inventory in inventoriesToAdd)
            {
                await _inventoryRepository.AddInventory(inventory);
            }

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while updating Sales Order.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
