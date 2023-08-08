using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.BuzonSugerencias.Commands;
using POS.Repository.BuzonSugerencias;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.BuzonSugerencias.Handlers
{
    public class UpdateBuzonSugerenciasCommandHandler : IRequestHandler<UpdateBuzonSugerenciasCommand, ServiceResponse<BuzonSugerenciasDto>>
    {
        private readonly IBuzonSugerenciasRepository _buzonSugerenciasRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBuzonSugerenciasCommandHandler> _logger;
        public UpdateBuzonSugerenciasCommandHandler(
           IBuzonSugerenciasRepository buzonSugerenciasRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateBuzonSugerenciasCommandHandler> logger
            )
        {
            _buzonSugerenciasRepository = buzonSugerenciasRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<BuzonSugerenciasDto>> Handle(UpdateBuzonSugerenciasCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _buzonSugerenciasRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            
            if (entityExist == null)
            {
                return ServiceResponse<BuzonSugerenciasDto>.Return404("La buzonSugerencias no existe");
            }

            entityExist = _mapper.Map(request, entityExist);

            _buzonSugerenciasRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<BuzonSugerenciasDto>.Return500();
            }

            return ServiceResponse<BuzonSugerenciasDto>.ReturnResultWith200(_mapper.Map<BuzonSugerenciasDto>(entityExist));
        }
    }
}
