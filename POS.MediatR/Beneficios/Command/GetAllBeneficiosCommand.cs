using MediatR;
using POS.Data;
using POS.Helper;
using POS.Repository;
using System;

namespace POS.MediatR.Beneficios.Command
{
    public class GetAllBeneficiosCommand : IRequest<BeneficiosList>
    {
        public BeneficiosResource Resource { get; set; }

        public Guid? IgnoreBeneficioCategoriaId { get; set; }
    }
}
