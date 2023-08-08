using MediatR;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, ServiceResponse<bool>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly ILogger<AddSupplierCommandHandler> _logger;
        public DeleteSupplierCommandHandler(
            ISupplierRepository supplierRepository,
            IPurchaseOrderRepository purchaseOrderRepository,
            PathHelper pathHelper,
            ILogger<AddSupplierCommandHandler> logger,
            IUnitOfWork<POSDbContext> uow)
        {
            _supplierRepository = supplierRepository;
            _purchaseOrderRepository = purchaseOrderRepository;
            _uow = uow;
            _pathHelper = pathHelper;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _supplierRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Supplier is not exist");
                return ServiceResponse<bool>.Return422("Supplier does not exist");
            }
            var exitingSupplier = _purchaseOrderRepository.All.Any(c => c.SupplierId == entityExist.Id);
            if (exitingSupplier)
            {
                _logger.LogError("Supplier can not be Deleted because it is use in Purchase Order");
                return ServiceResponse<bool>.Return409("Supplier can not be Deleted because it is use in Purchase Order");
            }

            _supplierRepository.Delete(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error to Delete Supplier");
                return ServiceResponse<bool>.Return500();
            }
            DeleteFile(entityExist.LogoUrl, _pathHelper.SupplierImagePath);
            DeleteFile(entityExist.BannerUrl, _pathHelper.SupplierImagePath);
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }

        public static void DeleteFile(string oldFile, string path)
        {
            string contentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!string.IsNullOrWhiteSpace(oldFile)
                && File.Exists(Path.Combine(contentRootPath, path, oldFile)))
            {
                FileData.DeleteFile(Path.Combine(contentRootPath, path, oldFile));
            }
        }
    }
}
