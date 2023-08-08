using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.EstadoCuenta.Command
{
    public class DeleteEstadoCuentaCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
