using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Data.Dto.Empresas;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Empresas.Command;
using POS.Repository.Empresas;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Empresas.Handler
{
    using POS.Data.Entities.Empresas;

    public class AddEmpresasCommandHandler : IRequestHandler<AddEmpresasCommand, ServiceResponse<EmpresasDto>>
    {
        private readonly IEmpresasRepository _empresasRepository;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddEmpresasCommandHandler(IEmpresasRepository empresasRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow)
        {
            _empresasRepository = empresasRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
        }

        public async Task<ServiceResponse<EmpresasDto>> Handle(AddEmpresasCommand request, CancellationToken cancellationToken)
        {
            var empresas = _mapper.Map<Empresas>(request);

            _empresasRepository.Add(empresas);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<EmpresasDto>.Return500();
            }

            await FileManager.SaveFile(empresas.UrlLogo, request.UrlLogoData, _pathHelper.EmpresasImagePath);

            return ServiceResponse<EmpresasDto>.ReturnResultWith201(_mapper.Map<EmpresasDto>(empresas));
        }
    }
}
