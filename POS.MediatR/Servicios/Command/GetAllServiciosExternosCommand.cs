using MediatR;
using POS.Data;
using POS.Helper;
using POS.Helper.Enum;
using POS.Repository;
using System;

namespace POS.MediatR.Servicios.Command
{
    public class GetAllServiciosExternosCommand : IRequest<ServiciosList>
    {
        public ServiciosExternosResource Resource { get; set; }

        public TipoServicio? TipoServicio { get; set; }
    }
}
