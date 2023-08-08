using MediatR;
using POS.Data;
using POS.Helper;
using POS.Repository;
using System;

namespace POS.MediatR.Beneficios.Command
{
    public class GetBeneficioCommand : IRequest<Data.Beneficios>
    {
        public Guid Id { get; set; }
    }
}
