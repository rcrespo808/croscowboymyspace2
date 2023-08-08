using AutoMapper;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;

namespace POS.Repository
{
    public class ServiciosCategoriasRepository : GenericRepository<ServiciosCategorias, POSDbContext>, IServiciosCategoriasRepository
    {
        public ServiciosCategoriasRepository(IUnitOfWork<POSDbContext> uow)
          : base(uow)
        {
        }
    }
}
