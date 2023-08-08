using AutoMapper;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Repository.Certificados;

namespace POS.Repository
{
    public class CertificadosRepository
        : GenericRepository<Data.Certificados, POSDbContext>, ICertificadosRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IInventoryHistoryRepository _inventoryHistoryRepository;
        private readonly IMapper _mapper;

        public CertificadosRepository(IUnitOfWork<POSDbContext> uow,
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
