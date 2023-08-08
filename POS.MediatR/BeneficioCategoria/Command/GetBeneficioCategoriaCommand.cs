using MediatR;
using POS.Data;
using System;

namespace POS.MediatR.BeneficioCategoria.Command
{
    public class GetBeneficioCategoriaCommand : IRequest<BeneficiosCategorias>
    {
        public Guid Id { get; set; }
    }
}
