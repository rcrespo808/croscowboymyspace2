using MediatR;
using POS.Data.Dto.Nosotros;
using POS.Helper;
using POS.Repository;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Nosotros.Command
{
    public class GetAllBeneficiosUsuariosCommandHandler : IRequestHandler<GetAllNosotrosCommand, NosotrosList>
    {
        private readonly INosotrosRepository _nosotrosRepository;
        private readonly PathHelper _pathHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllBeneficiosUsuariosCommandHandler(
            INosotrosRepository nosotrosRepository,
            PathHelper pathHelper,
            IPropertyMappingService propertyMappingService)
        {
            _nosotrosRepository = nosotrosRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<NosotrosList> Handle(GetAllNosotrosCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging =
                _nosotrosRepository.All.Select(c => new Data.Nosotros
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Location = c.Location,
                    ImageUrl = !string.IsNullOrWhiteSpace(c.ImageUrl) ? Path.Combine(_pathHelper.NosotrosImagePath, c.ImageUrl) : "",
                    CreatedDate = c.CreatedDate
                }).ApplySort(request.Resource.OrderBy,
                _propertyMappingService.GetPropertyMapping<NosotrosDto, Data.Nosotros>());
            var nosotrosList = new NosotrosList();
            return await nosotrosList.Create(collectionBeforePaging, request.Resource.Skip, request.Resource.PageSize);
        }
    }
}
