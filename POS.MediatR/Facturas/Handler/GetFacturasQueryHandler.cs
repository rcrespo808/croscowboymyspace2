using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Helper;
using POS.MediatR.Facturas.Command;
using POS.Repository.Facturas;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Facturas.Handler
{
    public class GetFacturasQueryHandler : IRequestHandler<GetFacturasQuery, FacturasDto>
    {
        private readonly IFacturasRepository _facturasRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;

        public GetFacturasQueryHandler(
            IFacturasRepository facturasRepository,
            PathHelper pathHelper,
            IMapper mapper)
        {
            _facturasRepository = facturasRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
        }
        public async Task<FacturasDto> Handle(GetFacturasQuery request, CancellationToken cancellationToken)
        {
            var facturas = await _facturasRepository
                                        .FindBy(facturas => facturas.Id == request.Id)
                                        .ProjectTo<FacturasDto>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync();

            return facturas;
        }
    }
}
