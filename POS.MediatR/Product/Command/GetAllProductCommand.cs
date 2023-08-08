using MediatR;
using POS.Data.Resources;
using POS.Repository;

namespace POS.MediatR.Product.Command
{
    public class GetAllProductCommand : IRequest<ProductList>
    {
        public ProductResource ProductResource { get; set; }
    }
}
