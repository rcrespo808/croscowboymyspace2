using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.Servicios.Command
{
    public class DeleteServicioCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
