using AutoMapper;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Domain;

namespace POS.Repository.Empresas
{
    using POS.Data.Entities.Empresas;
    public class EmpresasRepository
        : GenericRepository<Empresas, POSDbContext>, IEmpresasRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;

        public EmpresasRepository(IUnitOfWork<POSDbContext> uow,
            IPropertyMappingService propertyMappingService,
            IMapper mapper)
          : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }
    }
}
