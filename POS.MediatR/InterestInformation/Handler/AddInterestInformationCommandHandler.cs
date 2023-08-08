using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Dto.InterestInformation;
using POS.Data.Entities.Lookups;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Brand.Command;
using POS.MediatR.InterestInformation.Command;
using POS.MediatR.Tax.Commands;
using POS.Repository;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.InterestInformation.Handler
{
    public class AddInterestInformationCommandHandler : IRequestHandler<AddInterestInformationCommand, ServiceResponse<InterestInformationDto>>
    {
        private readonly IInterestInformationRepository _interestInformationRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddInterestInformationCommandHandler> _logger;
        private readonly PathHelper _pathHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AddInterestInformationCommandHandler(
           IInterestInformationRepository interestInformationRepository,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<AddInterestInformationCommandHandler> logger,
            PathHelper pathHelper,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _interestInformationRepository = interestInformationRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _pathHelper = pathHelper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ServiceResponse<InterestInformationDto>> Handle(AddInterestInformationCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _interestInformationRepository.FindBy(c => c.Titulo == request.Titulo).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                _logger.LogError("Interest Information Already Exist");
                return ServiceResponse<InterestInformationDto>.Return409("Interest Information Already Exist.");
            }
            var entity = _mapper.Map<Informacioninteres>(request);

            _interestInformationRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Save Interest Information have Error");
                return ServiceResponse<InterestInformationDto>.Return500();
            }

            await FileManager.SaveFile(entity.Logo, request.Logo, _pathHelper.InterestInformationLogos);
            await FileManager.SaveFile(entity.Documento, request.Documento, _pathHelper.InterestInformationDocuments);

            return ServiceResponse<InterestInformationDto>.ReturnResultWith200(_mapper.Map<InterestInformationDto>(entity));
        }
    }
}
