using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace POS.Data.Entities.Lookups
{
    public class Eventos : BaseEntity
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "La hora inicial no puede ser nulo.")]
        [DataMember(Name = "HoraInicial")]
        public TimeSpan HoraInicial { get; set; }

        [Required(ErrorMessage = "La hora final no puede ser nulo.")]
        [DataMember(Name = "HoraFinal")]
        public TimeSpan HoraFinal { get; set; }

        [Required(ErrorMessage = "La fecha inicial no puede ser nulo.")]
        [DataMember(Name = "FechaInicial")]
        public DateTime FechaInicial { get; set; }

        [Required(ErrorMessage = "Fecha final no puede ser nulo.")]
        [DataMember(Name = "FechaFinal")]
        public DateTime FechaFinal { get; set; }

        [Required(ErrorMessage = "La Costo socio no puede ser nulo.")]
        [Range(1, 99999999)]
        [DataMember(Name = "costoSocios")]
        public double CostoSocios { get; set; }

        [Required(ErrorMessage = "La costo comun no puede ser nulo.")]
        [Range(1, 99999999)]
        [DataMember(Name = "costoComun")]
        public double CostoComun { get; set; }

        [Required(ErrorMessage = "La ubicacion no puede ser nulo.")]
        [DataMember(Name = "ubicacion")]
        public string Ubicacion { get; set; }

        [Required]
        [DataMember(Name = "lat")]
        public double Lat { get; set; }

        [Required]
        [DataMember(Name = "lng")]
        public double Lng { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "estadoLInk")]
        public int EstadoLInk { get; set; }

        [Required(ErrorMessage = "La Titulo no puede ser nulo.")]
        [DataMember(Name = "titulo")]
        public string Titulo { get; set; }

        [DataMember(Name = "descripcion")]
        public string Descripcion { get; set; }

        [DataMember(Name = "banner1")]
        public string Banner1 { get; set; }

        [DataMember(Name = "banner2")]
        public string Banner2 { get; set; }

        [DataMember(Name = "adjunto")]
        public string Adjunto { get; set; }

        public bool Destacado { get; set; }

        public Guid ReminderId { get; set; }

        [NotMapped]
        public ICollection<Expone> Panelistas { get; set; }

        [NotMapped]
        public List<Asistencia> Asistencia { get; set; }
    }
}
