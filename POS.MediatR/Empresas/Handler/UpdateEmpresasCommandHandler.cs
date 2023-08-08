using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data.Dto.Empresas;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Empresas.Commands;
using POS.Repository.Empresas;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Empresas.Handlers
{
    public class UpdateEmpresasCommandHandler : IRequestHandler<UpdateEmpresasCommand, ServiceResponse<EmpresasDto>>
    {
        private readonly IEmpresasRepository _publicidadRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEmpresasCommandHandler> _logger;
        public UpdateEmpresasCommandHandler(
           IEmpresasRepository publicidadRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateEmpresasCommandHandler> logger
            )
        {
            _publicidadRepository = publicidadRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<EmpresasDto>> Handle(UpdateEmpresasCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _publicidadRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            
            if (entityExist == null)
            {
                return ServiceResponse<EmpresasDto>.Return404("La publicidad no existe");
            }

            var oldLogoUrl = entityExist.UrlLogo;
            entityExist = _mapper.Map(request, entityExist);

            _publicidadRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<EmpresasDto>.Return500();
            }

            await FileManager.UpdateFile(request.IsLogoChanged, _pathHelper.EmpresasImagePath, entityExist.UrlLogo, request.UrlLogoData, oldLogoUrl);
            
            return ServiceResponse<EmpresasDto>.ReturnResultWith200(_mapper.Map<EmpresasDto>(entityExist));
        }
    }
}
