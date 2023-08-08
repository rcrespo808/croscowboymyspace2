using MediatR;
using POS.Data;
using System;

namespace POS.MediatR.ServicioCategoria.Command
{
    public class GetServicioCategoriaCommand : IRequest<ServiciosCategorias>
    {
        public Guid Id { get; set; }
    }
}
