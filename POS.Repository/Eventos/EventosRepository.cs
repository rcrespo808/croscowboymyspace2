using AutoMapper;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Domain;

namespace POS.Repository
{
    public class EventosRepository
        : GenericRepository<Data.Entities.Lookups.Eventos, POSDbContext>, IEventosRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;

        public EventosRepository(IUnitOfWork<POSDbContext> uow,
            IPropertyMappingService propertyMappingService,
            IMapper mapper)
          : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }
    }
}
