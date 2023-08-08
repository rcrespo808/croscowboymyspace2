using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Helper;
using POS.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Beneficios.Command
{
    public class GetBeneficioCommandHandler : IRequestHandler<GetBeneficioCommand, Data.Beneficios>
    {
        private readonly IBeneficiosRepository _beneficiosRepository;
        private readonly PathHelper _pathHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetBeneficioCommandHandler(
            IBeneficiosRepository beneficiosRepository,
            PathHelper pathHelper,
            IPropertyMappingService propertyMappingService)
        {
            _beneficiosRepository = beneficiosRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<Data.Beneficios> Handle(GetBeneficioCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = await _beneficiosRepository
                .All
                .Include(b => b.BeneficioCategoria)
                .Where(b => b.Id == request.Id)
                .FirstOrDefaultAsync();

            return collectionBeforePaging;
        }
    }
}
