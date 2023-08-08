using MediatR;
using POS.Data.Dto;
using POS.Data.Dto.InterestInformation;
using POS.Data.Entities.Lookups;
using POS.Helper;
using System;
using System.Collections.Generic;

namespace POS.MediatR.InterestInformation.Command
{
    public class AddInterestInformationCommand : IRequest<ServiceResponse<InterestInformationDto>>
    {
        public string Tipo { get; set; }

        public string Logo { get; set; }

        public string Documento { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Contenido { get; set; }

        public DateTime Fecha { get; set; }

        public List<string> Etiquetas { get; set; }
    }
}
