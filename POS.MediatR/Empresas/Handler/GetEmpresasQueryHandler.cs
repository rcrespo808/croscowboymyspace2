using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto.Empresas;
using POS.Helper;
using POS.MediatR.Empresas.Command;
using POS.Repository.Empresas;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class GetEmpresasQueryHandler : IRequestHandler<GetEmpresasQuery, EmpresasDto>
    {
        private readonly IEmpresasRepository _empresasRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;

        public GetEmpresasQueryHandler(
            IEmpresasRepository empresasRepository,
            PathHelper pathHelper,
            IMapper mapper)
        {
            _empresasRepository = empresasRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
        }
        public async Task<EmpresasDto> Handle(GetEmpresasQuery request, CancellationToken cancellationToken)
        {
            var empresas = await _empresasRepository
                                        .FindBy(empresas => empresas.Id == request.Id)
                                        .ProjectTo<EmpresasDto>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync();

            return empresas;
        }
    }
}
