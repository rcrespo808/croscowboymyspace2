using MediatR;
using POS.Data.Dto;
using POS.Helper;
using POS.Helper.Enum.BuzonSugerencias;
using System;

namespace POS.MediatR.BuzonSugerencias.Command
{
    public class AddBuzonSugerenciasCommand : IRequest<ServiceResponse<BuzonSugerenciasDto>>
    {
        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public TipoSugerencia Tipo { get; set; }

        public string Tema { get; set; }

        public string Descripcion { get; set; }

        public EstadoSugerencia Estado { get; set; }

        public string Observaciones { get; set; }

        public Guid UserId { get; set; }

    }
}
