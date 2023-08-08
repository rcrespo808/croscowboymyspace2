using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class GetAllSupplierQueryPublicityHandler : IRequestHandler<GetAllSupplierPublicityQuery, SupplierList>
    {
        private readonly ISupplierRepository _supplierRepository;
        public GetAllSupplierQueryPublicityHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<SupplierList> Handle(GetAllSupplierPublicityQuery request, CancellationToken cancellationToken)
        {
            return await _supplierRepository.GetSuppliers(request.SupplierResource);
        }
    }
}
