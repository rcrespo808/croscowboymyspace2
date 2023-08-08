using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POS.Data.Dto
{
    public class EventosDto
    {
        [Key]
        public Guid Id { get; set; }

        public TimeSpan HoraInicial { get; set; }
        
        public TimeSpan HoraFinal { get; set; }

        public DateTime FechaInicial { get; set; }
        
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

        public bool IsRegistered { get; set; } = false;

        public bool Destacado { get; set; } = false;

        public ICollection<Customer> Panelistas { get; set; }

        public List<Asistencia> Asistencia { get; set; }
    }
}
