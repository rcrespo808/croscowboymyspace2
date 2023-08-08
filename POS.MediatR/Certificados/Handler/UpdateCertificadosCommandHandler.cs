using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Certificados.Commands;
using POS.Repository.Certificados;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Certificados.Handlers
{
    public class UpdateCertificadosCommandHandler : IRequestHandler<UpdateCertificadosCommand, ServiceResponse<CertificadosDto>>
    {
        private readonly ICertificadosRepository _certificadoRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCertificadosCommandHandler> _logger;
        public UpdateCertificadosCommandHandler(
           ICertificadosRepository certificadoRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateCertificadosCommandHandler> logger
            )
        {
            _certificadoRepository = certificadoRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<CertificadosDto>> Handle(UpdateCertificadosCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _certificadoRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            
            if (entityExist == null)
            {
                return ServiceResponse<CertificadosDto>.Return404("La certificado no existe");
            }

            entityExist = _mapper.Map(request, entityExist);

            _certificadoRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<CertificadosDto>.Return500();
            }

            return ServiceResponse<CertificadosDto>.ReturnResultWith200(_mapper.Map<CertificadosDto>(entityExist));
        }
    }
}
