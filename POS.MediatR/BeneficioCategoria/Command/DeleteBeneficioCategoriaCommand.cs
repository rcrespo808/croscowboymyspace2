using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.BeneficioCategoria.Command
{
    public class DeleteBeneficioCategoriaCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
