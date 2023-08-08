using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.Facturas.Command
{
    public class DeleteFacturasCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
