using AutoMapper;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class GetPurchaseOrderQueryHandler : IRequestHandler<GetPurchaseOrderQuery, ServiceResponse<PurchaseOrderDto>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IMapper _mapper;

        public GetPurchaseOrderQueryHandler(IPurchaseOrderRepository purchaseOrderRepository,
            IMapper mapper)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PurchaseOrderDto>> Handle(GetPurchaseOrderQuery request, CancellationToken cancellationToken)
        {
            var entity = await _purchaseOrderRepository.All
                .Include(c => c.PurchaseOrderPayments)
                .Include(c => c.Supplier)
                    //.ThenInclude(c => c.BillingAddress)
                .Include(c => c.PurchaseOrderItems)
                    .ThenInclude(c => c.PurchaseOrderItemTaxes)
                    .ThenInclude(cs => cs.Tax)
                .Include(c => c.PurchaseOrderItems)
                    .ThenInclude(c => c.Product)
                    .ThenInclude(c => c.Unit)
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<PurchaseOrderDto>(entity);
            return ServiceResponse<PurchaseOrderDto>.ReturnResultWith200(dto);
        }
    }
}
