using MediatR;
using POS.Data;
using POS.Helper;
using POS.Repository;
using System;

namespace POS.MediatR.Servicios.Command
{
    public class GetServicioCommand : IRequest<Data.Servicios>
    {
        public Guid Id { get; set; }
    }
}
