using MediatR;
using POS.Data.Dto;
using POS.Helper;
using System;

namespace POS.MediatR.Certificados.Command
{
    public class AddCertificadosCommand : IRequest<ServiceResponse<CertificadosDto>>
    {
        public string Nombre { get; set; }
        
        public string Ruta { get; set; }
        
        public Guid UserId { get; set; }

    }
}
