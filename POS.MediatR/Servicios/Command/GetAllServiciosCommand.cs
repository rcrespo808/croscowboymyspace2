using MediatR;
using POS.Data;
using POS.Helper;
using POS.Helper.Enum;
using POS.Repository;
using System;

namespace POS.MediatR.Servicios.Command
{
    public class GetAllServiciosCommand : IRequest<ServiciosList>
    {
        public ServiciosResource Resource { get; set; }

        public TipoServicio? TipoServicio { get; set; }
    }
}
