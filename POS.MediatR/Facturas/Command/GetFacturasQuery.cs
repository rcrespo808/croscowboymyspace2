using MediatR;
using POS.Data;
using System;

namespace POS.MediatR.Facturas.Command
{
    public class GetFacturasQuery : IRequest<FacturasDto>
    {
        public Guid Id { get; set; }
    }
}
