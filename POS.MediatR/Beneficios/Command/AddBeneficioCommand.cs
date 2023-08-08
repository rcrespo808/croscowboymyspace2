using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.Beneficios.Command
{
    public class AddBeneficioCommand : IRequest<ServiceResponse<Data.Beneficios>>
    {
        public string Nombre { get; set; }

        public bool TieneCupos { get; set; }

        public int Cupos { get; set; }

        public decimal Costo { get; set; }

        public decimal Descuento { get; set; }

        public string Color { get; set; }

        public string Descripcion { get; set; }

        public Guid BeneficioCategoriaId { get; set; }
    }
}
