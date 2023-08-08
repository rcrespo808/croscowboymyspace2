using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Unit.Commands;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Unit.Handlers
{
    public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, ServiceResponse<bool>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUnitCommandHandler> _logger;
        public UpdateUnitCommandHandler(
           IUnitRepository unitRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateUnitCommandHandler> logger
            )
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _unitRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id)
             .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Unit Already Exist.");
                return ServiceResponse<bool>.Return409("Unit Already Exist.");
            }
            entityExist = await _unitRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync();
            entityExist.Name = request.Name;
            _unitRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
