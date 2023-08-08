using AutoMapper;
using MediatR;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Facturas.Command;
using POS.Repository.Facturas;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Facturas.Handler
{
    public class AddFacturasCommandHandler : IRequestHandler<AddFacturasCommand, ServiceResponse<FacturasDto>>
    {
        private readonly IFacturasRepository _facturasRepository;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddFacturasCommandHandler(IFacturasRepository facturasRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow)
        {
            _facturasRepository = facturasRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
        }

        public async Task<ServiceResponse<FacturasDto>> Handle(AddFacturasCommand request, CancellationToken cancellationToken)
        {
            var facturas = _mapper.Map<Data.Facturas>(request);

            _facturasRepository.Add(facturas);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FacturasDto>.Return500();
            }

            return ServiceResponse<FacturasDto>.ReturnResultWith201(_mapper.Map<FacturasDto>(facturas));
        }
    }
}
