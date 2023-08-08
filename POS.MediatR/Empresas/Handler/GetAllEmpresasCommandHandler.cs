using AutoMapper;
using MediatR;
using POS.Helper;
using POS.Repository;
using POS.Repository.Empresas;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Empresas.Command
{
    public class GetAllEmpresasCommandHandler : IRequestHandler<GetAllEmpresasCommand, EmpresasList>
    {
        private readonly IEmpresasRepository _empresasRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllEmpresasCommandHandler(
            IEmpresasRepository empresasRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _empresasRepository = empresasRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }

        public async Task<EmpresasList> Handle(GetAllEmpresasCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = _empresasRepository.All;
            
            //if (!string.IsNullOrEmpty(request.Resource.Nombre))
            //{
            //    // trim & ignore casing
            //    var genreForWhereClause = request.Resource.Nombre
            //        .Trim().ToLowerInvariant();
            //    var name = Uri.UnescapeDataString(genreForWhereClause);
            //    var encodingName = WebUtility.UrlDecode(name);
            //    var ecapestring = Regex.Unescape(encodingName);
            //    encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
            //    collectionBeforePaging = collectionBeforePaging
            //        .Where(a => EF.Functions.Like(a.Nombre, $"%{encodingName}%"));
            //}

            var empresasList = new EmpresasList(_mapper);
            return await empresasList.Create(collectionBeforePaging, request.Resource.Skip, request.Resource.PageSize);
        }
    }
}
