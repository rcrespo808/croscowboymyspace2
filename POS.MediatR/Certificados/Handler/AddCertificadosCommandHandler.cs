using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Certificados.Command;
using POS.Repository.Certificados;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Certificados.Handler
{
    public class AddCertificadosCommandHandler : IRequestHandler<AddCertificadosCommand, ServiceResponse<CertificadosDto>>
    {
        private readonly ICertificadosRepository _certificadosRepository;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddCertificadosCommandHandler(ICertificadosRepository certificadosRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow)
        {
            _certificadosRepository = certificadosRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
        }

        public async Task<ServiceResponse<CertificadosDto>> Handle(AddCertificadosCommand request, CancellationToken cancellationToken)
        {
            var certificados = _mapper.Map<Data.Certificados>(request);

            _certificadosRepository.Add(certificados);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<CertificadosDto>.Return500();
            }

            return ServiceResponse<CertificadosDto>.ReturnResultWith201(_mapper.Map<CertificadosDto>(certificados));
        }
    }
}
