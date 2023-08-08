using MediatR;
using POS.Data;
using POS.Helper;
using System;

namespace POS.MediatR.Facturas.Command
{
    public class AddFacturasCommand : IRequest<ServiceResponse<FacturasDto>>
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaPago { get; set; }

        public string UrlArchivo { get; set; }

        public Guid IdUsuario { get; set; }
    }
}
