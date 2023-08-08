using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.ServicioCategoria.Command
{
    public class DeleteServicioCategoriaCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
