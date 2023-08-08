using MediatR;
using POS.Data.Resources;
using POS.Repository.Certificados;

namespace POS.MediatR.Certificados.Command
{
    public class GetAllCertificadosCommand : IRequest<CertificadosList>
    {
        public CertificadosResource Resource { get; set; }
    }
}
