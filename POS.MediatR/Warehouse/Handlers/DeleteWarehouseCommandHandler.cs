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
using POS.MediatR.Warehouse.Commands;

namespace POS.MediatR.Warehouse.Handlers
{
    public class DeleteWarehouseCommandHandler
        : IRequestHandler<DeleteWarehouseCommand, ServiceResponse<bool>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUnitOfWork<POSDbContext> _wow;
        private readonly ILogger<DeleteWarehouseCommand> _logger;

        public DeleteWarehouseCommandHandler(
           IWarehouseRepository warehouseRepository,
            IUnitOfWork<POSDbContext> wow,
            ILogger<DeleteWarehouseCommand> logger
            )
        {
            _warehouseRepository = warehouseRepository;
            _wow = wow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _warehouseRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Warehouse Does not exists");
                return ServiceResponse<bool>.Return404("Warehouse Does not exists");
            }

            _warehouseRepository.Delete(entityExist);
            if (await _wow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While saving Warehouse.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
