using MediatR;
using POS.Data;
using System;

namespace POS.MediatR.EstadoCuenta.Command
{
    public class GetEstadoCuentaQuery : IRequest<EstadoCuentaDto>
    {
        public Guid Id { get; set; }
    }
}
