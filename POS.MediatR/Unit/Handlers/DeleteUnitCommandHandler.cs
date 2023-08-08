using POS.Common.UnitOfWork;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using POS.MediatR.Unit.Commands;

namespace POS.MediatR.Unit.Handlers
{
    public class DeleteUnitCommandHandler
        : IRequestHandler<DeleteUnitCommand, ServiceResponse<bool>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly ILogger<DeleteUnitCommandHandler> _logger;

        public DeleteUnitCommandHandler(
           IUnitRepository unitRepository,
             IProductRepository productRepository,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeleteUnitCommandHandler> logger
            )
        {
            _unitRepository = unitRepository;
            _productRepository = productRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _unitRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Product Unit Does not exists");
                return ServiceResponse<bool>.Return404("Product Unit Does not exists");
            }

            var exitingProduct = _productRepository.AllIncluding(c => c.Unit).Any(c => c.Unit.Id == entityExist.Id);
            if (exitingProduct)
            {
                _logger.LogError("Unit can not be Deleted because it is use in product");
                return ServiceResponse<bool>.Return409("Unit can not be Deleted because it is use in product.");
            }

            _unitRepository.Delete(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While saving Product Unit.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
