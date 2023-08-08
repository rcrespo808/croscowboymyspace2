using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;

namespace POS.Repository
{
    public class UnitRepository : GenericRepository<Unit, POSDbContext>, IUnitRepository
    {
        public UnitRepository(IUnitOfWork<POSDbContext> uow)
          : base(uow)
        {
        }
    }
}
