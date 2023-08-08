using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Certificados.Command;
using POS.Repository.Certificados;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Certificados.Handler
{
    public class GetCertificadosQueryHandler : IRequestHandler<GetCertificadosQuery, CertificadosDto>
    {
        private readonly ICertificadosRepository _certificadosRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;

        public GetCertificadosQueryHandler(
            ICertificadosRepository certificadosRepository,
            PathHelper pathHelper,
            IMapper mapper)
        {
            _certificadosRepository = certificadosRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
        }
        public async Task<CertificadosDto> Handle(GetCertificadosQuery request, CancellationToken cancellationToken)
        {
            var certificados = await _certificadosRepository
                                        .FindBy(certificados => certificados.Id == request.Id)
                                        .ProjectTo<CertificadosDto>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync();

            return certificados;
        }
    }
}
