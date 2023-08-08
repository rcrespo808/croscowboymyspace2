using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Beneficios.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Beneficio.Handler
{
    public class AddBeneficioCommandHanlder
        : IRequestHandler<AddBeneficioCommand, ServiceResponse<Data.Beneficios>>
    {

        private readonly IBeneficiosRepository _beneficioRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddBeneficioCommandHanlder(IBeneficiosRepository beneficioRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow)
        {
            _beneficioRepository = beneficioRepository;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<Data.Beneficios>> Handle(AddBeneficioCommand request, CancellationToken cancellationToken)
        {
            var existingBeneficio = await _beneficioRepository.All
                .FirstOrDefaultAsync(c => c.Nombre == request.Nombre);
            if (existingBeneficio != null)
            {
                return ServiceResponse<Data.Beneficios>.Return409("Existe un beneficio con el mismo nombre");
            }

            var beneficio = _mapper.Map<Data.Beneficios>(request);
            _beneficioRepository.Add(beneficio);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<Data.Beneficios>.Return500();
            }

            return ServiceResponse<Data.Beneficios>.ReturnResultWith201(beneficio);
        }
    }
}
