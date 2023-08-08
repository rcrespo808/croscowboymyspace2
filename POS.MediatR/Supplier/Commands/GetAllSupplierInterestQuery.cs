using POS.Data.Resources;
using POS.Repository;
using MediatR;

namespace POS.MediatR.CommandAndQuery
{
    public class GetAllSupplierInterestQuery : IRequest<SupplierList>
    {
        public SupplierResource SupplierResource { get; set; }
    }
}
