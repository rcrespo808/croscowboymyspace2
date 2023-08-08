using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.PurchaseOrder.Commands;
using POS.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.PurchaseOrder.Handlers
{
    public class UpdatePurchaseOrderCommandHandler : IRequestHandler<UpdatePurchaseOrderCommand, ServiceResponse<PurchaseOrderDto>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IPurchaseOrderItemRepository _purchaseOrderItemRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePurchaseOrderCommandHandler> _logger;
        private readonly IInventoryRepository _inventoryRepository;

        public UpdatePurchaseOrderCommandHandler(
            IPurchaseOrderRepository purchaseOrderRepository,
            IPurchaseOrderItemRepository purchaseOrderItemRepository,
            IUnitOfWork<POSDbContext> uow,
            IMapper mapper,
            ILogger<UpdatePurchaseOrderCommandHandler> logger,
            IInventoryRepository inventoryRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _purchaseOrderItemRepository = purchaseOrderItemRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ServiceResponse<PurchaseOrderDto>> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var existingPONumber = _purchaseOrderRepository.All.Any(c => c.OrderNumber == request.OrderNumber && c.Id != request.Id);
            if (existingPONumber)
            {
                return ServiceResponse<PurchaseOrderDto>.Return409("Purchase Order Number is already Exists.");
            }
            var purchaseOrderItemsExist = await _purchaseOrderItemRepository.FindBy(c => c.PurchaseOrderId == request.Id).ToListAsync();
            _purchaseOrderItemRepository.RemoveRange(purchaseOrderItemsExist);

            var purchaseOrderUpdate = _mapper.Map<POS.Data.PurchaseOrder>(request);

            var purchaseOrderExit = await _purchaseOrderRepository.FindAsync(request.Id);
            if (purchaseOrderExit.Status == Data.PurchaseOrderStatus.Return)
            {
                return ServiceResponse<PurchaseOrderDto>.Return409("Purchase Order can't edit becuase it's already approved.");
            }

            purchaseOrderExit.OrderNumber = purchaseOrderUpdate.OrderNumber;
            purchaseOrderExit.SupplierId = purchaseOrderUpdate.SupplierId;
            purchaseOrderExit.Note = purchaseOrderUpdate.Note;
            purchaseOrderExit.TermAndCondition = purchaseOrderUpdate.TermAndCondition;
            purchaseOrderExit.IsPurchaseOrderRequest = purchaseOrderUpdate.IsPurchaseOrderRequest;
            purchaseOrderExit.POCreatedDate = purchaseOrderUpdate.POCreatedDate;
            purchaseOrderExit.Status = purchaseOrderUpdate.Status;
            purchaseOrderExit.DeliveryDate = purchaseOrderUpdate.DeliveryDate;
            purchaseOrderExit.DeliveryStatus = purchaseOrderUpdate.DeliveryStatus;
            purchaseOrderExit.SupplierId = purchaseOrderUpdate.SupplierId;
            purchaseOrderExit.TotalAmount = purchaseOrderUpdate.TotalAmount;
            purchaseOrderExit.TotalTax = purchaseOrderUpdate.TotalTax;
            purchaseOrderExit.TotalDiscount = purchaseOrderUpdate.TotalDiscount;
            purchaseOrderExit.PurchaseOrderItems = purchaseOrderUpdate.PurchaseOrderItems;
            purchaseOrderExit.PurchaseOrderItems.ForEach(c =>
            {
                c.PurchaseOrderId = purchaseOrderUpdate.Id;
            });
            purchaseOrderExit.PurchaseOrderItems.ForEach(item =>
            {
                item.Product = null;
                item.PurchaseOrderItemTaxes.ForEach(tax => { tax.Tax = null; });
            });
            _purchaseOrderRepository.Update(purchaseOrderExit);

            if (!request.IsPurchaseOrderRequest)
            {
                var inventoriesToAdd = request.PurchaseOrderItems
                .GroupBy(c => c.ProductId)
                .Select(cs => new InventoryDto
                {
                    InventorySource = Data.InventorySourceEnum.PurchaseOrder,
                    ProductId = cs.Key,
                    PricePerUnit = cs.Sum(d => d.UnitPrice * d.Quantity + d.TaxValue - d.Discount) / cs.Sum(d => d.Quantity),
                    PurchaseOrderId = purchaseOrderExit.Id,
                    Stock = cs.Sum(d => d.Quantity),
                }).ToList();
                foreach (var inventory in inventoriesToAdd)
                {
                    await _inventoryRepository.AddInventory(inventory);
                }
            }

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while creating Purchase Order.");
                return ServiceResponse<PurchaseOrderDto>.Return500();
            }
            var dto = _mapper.Map<PurchaseOrderDto>(purchaseOrderExit);
            return ServiceResponse<PurchaseOrderDto>.ReturnResultWith201(dto);
        }
    }
}
