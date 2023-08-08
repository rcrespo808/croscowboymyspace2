using MediatR;
using POS.Helper;
using System;
namespace POS.MediatR.Publicidad.Command
{
    public class DeletePublicidadCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
