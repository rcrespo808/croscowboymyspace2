using MediatR;
using POS.Data;
using POS.Helper;
using System;

namespace POS.MediatR.ServicioCategoria.Command
{
    public class AddServicioCategoriaCommand : IRequest<ServiceResponse<ServiciosCategorias>>
    {
        public string Nombre { get; set; }

        public string UrlImageData { get; set; }
    }
}
