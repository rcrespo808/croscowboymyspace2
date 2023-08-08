using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Unit.Commands;
using POS.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Unit.Handlers
{
    public class AddUnitCommandHandler : IRequestHandler<AddUnitCommand, ServiceResponse<UnitDto>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUnitCommandHandler> _logger;
        public AddUnitCommandHandler(
           IUnitRepository unitRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<AddUnitCommandHandler> logger
            )
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<UnitDto>> Handle(AddUnitCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _unitRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                _logger.LogError("Unit Already Exist");
                return ServiceResponse<UnitDto>.Return409("Unit Already Exist.");
            }
            var entity = _mapper.Map<POS.Data.Unit>(request);
            entity.Id = Guid.NewGuid();
            _unitRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Save Page have Error");
                return ServiceResponse<UnitDto>.Return500();
            }
            return ServiceResponse<UnitDto>.ReturnResultWith200(_mapper.Map<UnitDto>(entity));
        }
    }
}
