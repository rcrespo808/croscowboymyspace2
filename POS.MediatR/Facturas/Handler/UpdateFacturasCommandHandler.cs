using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Facturas.Commands;
using POS.Repository.Facturas;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Facturas.Handlers
{
    public class UpdateFacturasCommandHandler : IRequestHandler<UpdateFacturasCommand, ServiceResponse<FacturasDto>>
    {
        private readonly IFacturasRepository _facturasRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFacturasCommandHandler> _logger;
        public UpdateFacturasCommandHandler(
           IFacturasRepository facturasRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateFacturasCommandHandler> logger
            )
        {
            _facturasRepository = facturasRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<FacturasDto>> Handle(UpdateFacturasCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _facturasRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            
            if (entityExist == null)
            {
                return ServiceResponse<FacturasDto>.Return404("La facturas no existe");
            }

            entityExist = _mapper.Map(request, entityExist);

            _facturasRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FacturasDto>.Return500();
            }

            return ServiceResponse<FacturasDto>.ReturnResultWith200(_mapper.Map<FacturasDto>(entityExist));
        }
    }
}
