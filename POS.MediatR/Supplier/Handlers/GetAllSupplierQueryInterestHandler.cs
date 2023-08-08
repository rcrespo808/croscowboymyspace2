using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class GetAllSupplierQueryInterestHandler : IRequestHandler<GetAllSupplierInterestQuery, SupplierList>
    {
        private readonly ISupplierRepository _supplierRepository;
        public GetAllSupplierQueryInterestHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<SupplierList> Handle(GetAllSupplierInterestQuery request, CancellationToken cancellationToken)
        {
            return await _supplierRepository.GetSuppliers(request.SupplierResource);
        }
    }
}
