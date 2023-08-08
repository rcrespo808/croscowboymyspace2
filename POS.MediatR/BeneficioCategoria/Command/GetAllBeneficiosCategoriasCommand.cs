using MediatR;
using POS.Data;
using System;
using System.Collections.Generic;

namespace POS.MediatR.BeneficioCategoria.Command
{
    public class GetAllBeneficiosCategoriasCommand : IRequest<List<BeneficiosCategorias>>
    {
        public Guid? IgnoreBeneficioCategoriaId { get; set; }
    }
}
