using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.Facturas.Commands
{
    public class UpdateFacturasCommand : IRequest<ServiceResponse<Data.FacturasDto>>
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaPago { get; set; }

        public string UrlArchivo { get; set; }

        public Guid IdUsuario { get; set; }
    }
}
