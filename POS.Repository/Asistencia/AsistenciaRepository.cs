using AutoMapper;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;

namespace POS.Repository
{
    public class AsistenciaRepository
        : GenericRepository<Asistencia, POSDbContext>, IAsistenciaRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;

        public AsistenciaRepository(IUnitOfWork<POSDbContext> uow,
            IPropertyMappingService propertyMappingService,
            IMapper mapper)
          : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }
    }
}
