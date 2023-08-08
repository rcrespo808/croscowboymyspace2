using MediatR;
using POS.Data;
using POS.Helper;
using System;
using System.Collections.Generic;

namespace POS.MediatR.Eventos.Command
{
    public class UpdateEventosCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }

        public TimeSpan HoraInicial { get; set; }

        public DateTime FechaInicial { get; set; }

        public TimeSpan HoraFinal { get; set; }

        public DateTime FechaFinal { get; set; }

        public double CostoSocios { get; set; }

        public double CostoComun { get; set; }

        public string Ubicacion { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Link { get; set; }

        public int EstadoLInk { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Banner1 { get; set; }

        public string Banner2 { get; set; }

        public string Adjunto { get; set; }

        public bool Destacado { get; set; }

        public ICollection<Customers> Panelistas { get; set; }

        public bool IsBanner1Changed { get; set; }

        public bool IsBanner2Changed { get; set; }

        public bool IsAdjuntoChanged { get; set; }

    }
}
