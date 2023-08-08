using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class GetPublicidadQueryHandler : IRequestHandler<GetPublicidadQuery, PublicidadDto>
    {
        private readonly IPublicidadRepository _publicidadRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;

        public GetPublicidadQueryHandler(
            IPublicidadRepository publicidadRepository,
            PathHelper pathHelper,
            IMapper mapper)
        {
            _publicidadRepository = publicidadRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
        }
        public async Task<PublicidadDto> Handle(GetPublicidadQuery request, CancellationToken cancellationToken)
        {
            var publicidad = await _publicidadRepository
                                        .FindBy(publicidad => publicidad.Id == request.Id)
                                        .ProjectTo<PublicidadDto>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync();

            return publicidad;
        }
    }
}
