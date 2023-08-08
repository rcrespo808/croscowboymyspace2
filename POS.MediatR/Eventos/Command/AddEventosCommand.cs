using MediatR;
using POS.Data;
using POS.Helper;
using System.Collections.Generic;
using System;

namespace POS.MediatR.Eventos.Command
{
    public class AddEventosCommand : IRequest<ServiceResponse<bool>>
    {
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

        public IEnumerable<Customers> Panelistas { get; set; }
    }
}
