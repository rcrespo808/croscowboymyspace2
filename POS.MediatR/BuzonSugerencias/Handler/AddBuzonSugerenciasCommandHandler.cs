using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.BuzonSugerencias.Command;
using POS.Repository.BuzonSugerencias;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.BuzonSugerencias.Handler
{
    public class AddBuzonSugerenciasCommandHandler : IRequestHandler<AddBuzonSugerenciasCommand, ServiceResponse<BuzonSugerenciasDto>>
    {
        private readonly IBuzonSugerenciasRepository _buzonSugerenciasRepository;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddBuzonSugerenciasCommandHandler(IBuzonSugerenciasRepository buzonSugerenciasRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow)
        {
            _buzonSugerenciasRepository = buzonSugerenciasRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
        }

        public async Task<ServiceResponse<BuzonSugerenciasDto>> Handle(AddBuzonSugerenciasCommand request, CancellationToken cancellationToken)
        {
            var buzonSugerencias = _mapper.Map<Data.BuzonSugerencias>(request);

            _buzonSugerenciasRepository.Add(buzonSugerencias);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<BuzonSugerenciasDto>.Return500();
            }

            return ServiceResponse<BuzonSugerenciasDto>.ReturnResultWith201(_mapper.Map<BuzonSugerenciasDto>(buzonSugerencias));
        }
    }
}
