using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Beneficios.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Beneficio.Handler
{
    public class UpdateBeneficioCommandHandler
      : IRequestHandler<UpdateBeneficioCommand, ServiceResponse<Data.Beneficios>>
    {

        private readonly IBeneficiosRepository _beneficioRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<UpdateBeneficioCommandHandler> _logger;

        public UpdateBeneficioCommandHandler(
            IBeneficiosRepository beneficioRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            PathHelper pathHelper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<UpdateBeneficioCommandHandler> logger)
        {
            _beneficioRepository = beneficioRepository;
            _mapper = mapper;
            _uow = uow;
            _pathHelper = pathHelper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public async Task<ServiceResponse<Data.Beneficios>> Handle(UpdateBeneficioCommand request, CancellationToken cancellationToken)
        {
            var existingBeneficio = await _beneficioRepository.All.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (existingBeneficio == null)
            {
                return ServiceResponse<Data.Beneficios>.Return404("El beneficios no existe");
            }

            existingBeneficio = _mapper.Map(request, existingBeneficio);
            _beneficioRepository.Update(existingBeneficio);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Hubo un error al momento de actualizar el beneficio");
                return ServiceResponse<Data.Beneficios>.Return500();
            }

            return ServiceResponse<Data.Beneficios>.ReturnResultWith201(existingBeneficio);
        }
    }
}
