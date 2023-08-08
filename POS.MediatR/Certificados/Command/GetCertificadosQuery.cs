using MediatR;
using POS.Data.Dto;
using System;

namespace POS.MediatR.Certificados.Command
{
    public class GetCertificadosQuery : IRequest<CertificadosDto>
    {
        public Guid Id { get; set; }
    }
}
