using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto.Nosotros;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class GetNosotrosQueryHandler : IRequestHandler<GetNosotrosQuery, NosotrosDto>
    {
        private readonly INosotrosRepository _nosotrosRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;

        public GetNosotrosQueryHandler(
            INosotrosRepository nosotrosRepository,
            PathHelper pathHelper,
            IMapper mapper)
        {
            _nosotrosRepository = nosotrosRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
        }
        public async Task<NosotrosDto> Handle(GetNosotrosQuery request, CancellationToken cancellationToken)
        {
            var nosotros = await _nosotrosRepository.FindBy(nosotros => nosotros.Id == request.Id)
                .Select(c => new Data.Nosotros
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Location = c.Location,
                    ImageUrl = !string.IsNullOrWhiteSpace(c.ImageUrl) ? Path.Combine(_pathHelper.NosotrosImagePath, c.ImageUrl) : ""
                })
                .FirstOrDefaultAsync();

            return _mapper.Map<Data.Nosotros, NosotrosDto>(nosotros);
        }
    }
}
