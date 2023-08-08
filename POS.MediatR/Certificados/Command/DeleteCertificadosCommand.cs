using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.Certificados.Command
{
    public class DeleteCertificadosCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
