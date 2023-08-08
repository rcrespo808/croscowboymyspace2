using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data.Dto.InterestInformation;
using POS.Data.Entities.Lookups;
using POS.Domain;
using POS.Helper;
using POS.MediatR.InterestInformation.Command;
using POS.Repository;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.InterestInformation.Handler
{
    public class UpdateInterestInformationCommandHandler
        : IRequestHandler<UpdateInterestInformationCommand, ServiceResponse<InterestInformationDto>>
    {
        private readonly IInterestInformationRepository _interestInformationRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<UpdateInterestInformationCommand> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        public UpdateInterestInformationCommandHandler(
           IInterestInformationRepository interestInformationRepository,
            IUnitOfWork<POSDbContext> uow,
            ILogger<UpdateInterestInformationCommand> logger,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper,
            IMapper mapper
            )
        {
            _interestInformationRepository = interestInformationRepository;
            _uow = uow;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<InterestInformationDto>> Handle(UpdateInterestInformationCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _interestInformationRepository.FindBy(c => c.Titulo == request.Titulo && c.Id != request.Id)
             .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Interest Information Already Exist.");
                return ServiceResponse<InterestInformationDto>.Return409("Interest Information Already Exist.");
            }
            entityExist = await _interestInformationRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync();

            var oldDocument = entityExist.Documento;
            var oldLogo = entityExist.Logo;

            entityExist = _mapper.Map(request, entityExist);

            _interestInformationRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<InterestInformationDto>.Return500();
            }

            await FileManager.UpdateFile(request.IsLogoChanged, _pathHelper.InterestInformationLogos, entityExist.Logo, request.Logo, oldLogo);
            await FileManager.UpdateFile(request.IsDocumentChanged, _pathHelper.InterestInformationDocuments, entityExist.Documento, request.Documento, oldDocument);

            return ServiceResponse<InterestInformationDto>.ReturnResultWith200(_mapper.Map<InterestInformationDto>(entityExist));
        }
    }
}
