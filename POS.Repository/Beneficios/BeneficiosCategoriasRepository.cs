using AutoMapper;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;

namespace POS.Repository
{
    public class BeneficiosCategoriasRepository : GenericRepository<BeneficiosCategorias, POSDbContext>, IBeneficiosCategoriasRepository
    {
        public BeneficiosCategoriasRepository(IUnitOfWork<POSDbContext> uow)
          : base(uow)
        {
        }
    }
}
