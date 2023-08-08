using POS.Data.Resources;
using POS.Repository;
using MediatR;

namespace POS.MediatR.CommandAndQuery
{
    public class GetAllSupplierPublicityQuery : IRequest<SupplierList>
    {
        public SupplierResource SupplierResource { get; set; }
    }
}
