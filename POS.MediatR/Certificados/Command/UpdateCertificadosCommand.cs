using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.Certificados.Commands
{
    public class UpdateCertificadosCommand : IRequest<ServiceResponse<Data.Dto.CertificadosDto>>
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Ruta { get; set; }

        public Guid UserId { get; set; }
    }
}
