using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data.Dto.InterestInformation
{
    public class InterestInformationDto
    {
        public Guid Id { get; set; }

        public string Tipo { get; set; }

        public string Logo { get; set; }

        public string Documento { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Contenido { get; set; }

        public DateTime Fecha { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<string> Etiquetas { get; set; }
    }
}