using MediatR;
using POS.Data;
using POS.Helper;
using System;

namespace POS.MediatR.ServicioCategoria.Command
{
    public class UpdateServicioCategoriaCommand : IRequest<ServiceResponse<ServiciosCategorias>>
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string UrlImageData { get; set; }

        public bool IsImageUpdated { get; set; }
    }
}
