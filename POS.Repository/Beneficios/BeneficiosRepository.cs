using AutoMapper;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;

namespace POS.Repository
{
    public class BeneficiosRepository : GenericRepository<Beneficios, POSDbContext>, IBeneficiosRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IInventoryHistoryRepository _inventoryHistoryRepository;
        private readonly IMapper _mapper;

        public BeneficiosRepository(IUnitOfWork<POSDbContext> uow,
            IPropertyMappingService propertyMappingService,
            IInventoryHistoryRepository inventoryHistoryRepository,
            IMapper mapper)
          : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _inventoryHistoryRepository = inventoryHistoryRepository;
            _mapper = mapper;
        }
    }
}
