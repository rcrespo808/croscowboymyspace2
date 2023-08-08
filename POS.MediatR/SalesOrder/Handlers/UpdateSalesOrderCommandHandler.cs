using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.SalesOrder.Commands;
using POS.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class UpdateSalesOrderCommandHandler
        : IRequestHandler<UpdateSalesOrderCommand, ServiceResponse<SalesOrderDto>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly ISalesOrderItemRepository _salesOrderItemRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSalesOrderCommandHandler> _logger;
        private readonly IInventoryRepository _inventoryRepository;

        public UpdateSalesOrderCommandHandler(
            ISalesOrderRepository salesOrderRepository,
            ISalesOrderItemRepository salesOrderItemRepository,
            IUnitOfWork<POSDbContext> uow,
            IMapper mapper,
            ILogger<UpdateSalesOrderCommandHandler> logger,
            IInventoryRepository inventoryRepository)
        {
            _salesOrderRepository = salesOrderRepository;
            _salesOrderItemRepository = salesOrderItemRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ServiceResponse<SalesOrderDto>> Handle(UpdateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var existingSONumber = _salesOrderRepository.All.Any(c => c.OrderNumber == request.OrderNumber && c.Id != request.Id);
            if (existingSONumber)
            {
                return ServiceResponse<SalesOrderDto>.Return409("Sales Order Number is already Exists.");
            }
            var salesOrderItemsExist = await _salesOrderItemRepository.FindBy(c => c.SalesOrderId == request.Id).ToListAsync();
            _salesOrderItemRepository.RemoveRange(salesOrderItemsExist);

            var salesOrderUpdate = _mapper.Map<POS.Data.SalesOrder>(request);
            salesOrderUpdate.SalesOrderItems.ForEach(item =>
            {
                item.Product = null;
                item.SalesOrderItemTaxes.ForEach(tax => { tax.Tax = null; });
            });
            var salesOrderExit = await _salesOrderRepository.FindAsync(request.Id);
            if (salesOrderExit.Status == Data.SalesOrderStatus.Return)
            {
                return ServiceResponse<SalesOrderDto>.Return409("Sales Order can't edit becuase it's already Return.");
            }

            salesOrderExit.OrderNumber = salesOrderUpdate.OrderNumber;
            salesOrderExit.CustomerId = salesOrderUpdate.CustomerId;
            salesOrderExit.Note = salesOrderUpdate.Note;
            salesOrderExit.TermAndCondition = salesOrderUpdate.TermAndCondition;
            salesOrderExit.IsSalesOrderRequest = salesOrderUpdate.IsSalesOrderRequest;
            salesOrderExit.SOCreatedDate = salesOrderUpdate.SOCreatedDate;
            salesOrderExit.Status = salesOrderUpdate.Status;
            salesOrderExit.DeliveryDate = salesOrderUpdate.DeliveryDate;
            salesOrderExit.DeliveryStatus = salesOrderUpdate.DeliveryStatus;
            salesOrderExit.CustomerId = salesOrderUpdate.CustomerId;
            salesOrderExit.TotalAmount = salesOrderUpdate.TotalAmount;
            salesOrderExit.TotalTax = salesOrderUpdate.TotalTax;
            salesOrderExit.TotalDiscount = salesOrderUpdate.TotalDiscount;
            salesOrderExit.SalesOrderItems = salesOrderUpdate.SalesOrderItems;
            salesOrderExit.SalesOrderItems.ForEach(c =>
            {
                c.SalesOrderId = salesOrderUpdate.Id;
                c.CreatedDate = DateTime.UtcNow;
            });

            _salesOrderRepository.Update(salesOrderExit);

            var inventoriesToAdd = request.SalesOrderItems
                .GroupBy(c => c.ProductId)
                .Select(cs => new InventoryDto
                {
                    InventorySource = Data.InventorySourceEnum.SalesOrder,
                    ProductId = cs.Key,
                    PricePerUnit = cs.Sum(d => d.UnitPrice * d.Quantity + d.TaxValue - d.Discount) / cs.Sum(d => d.Quantity),
                    SalesOrderId = salesOrderExit.Id,
                    Stock = cs.Sum(d => d.Quantity),
                }).ToList();

            foreach (var inventory in inventoriesToAdd)
            {
                await _inventoryRepository.AddInventory(inventory);
            }

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while Updating Sales Order.");
                return ServiceResponse<SalesOrderDto>.Return500();
            }
            var dto = _mapper.Map<SalesOrderDto>(salesOrderExit);
            return ServiceResponse<SalesOrderDto>.ReturnResultWith201(dto);
        }
    }

}
