using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.Beneficios.Command
{
    public class DeleteBeneficioCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
