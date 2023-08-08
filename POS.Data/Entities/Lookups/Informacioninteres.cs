using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace POS.Data.Entities.Lookups
{
    public class Informacioninteres : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Tipo { get; set; }

        public string Logo { get; set; }

        [Required]
        public string Documento { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public string Contenido { get; set; }

        public DateTime Fecha { get; set; }

        public string Etiquetas { get; set; }
    }
}