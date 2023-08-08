using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Category.Commands;
using POS.Repository;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Category.Handlers
{
    public class DeleteProductCategoryCommandHandler
        : IRequestHandler<DeleteProductCategoryCommand, ServiceResponse<bool>>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProductCategoryCommandHandler> _logger;
        public DeleteProductCategoryCommandHandler(
           IProductCategoryRepository productCategoryRepository,
           IProductRepository productRepository,
            PathHelper pathHelper,
            IMapper mapper,
            IUnitOfWork<POSDbContext> uow,
            ILogger<DeleteProductCategoryCommandHandler> logger
            )
        {
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
            _pathHelper = pathHelper;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _productCategoryRepository
                .FindAsync(request.Id);
           
            if (existingEntity == null)
            {
                _logger.LogError("Product Category not Exists");
                return ServiceResponse<bool>.Return409("Product Category not Exists.");
            }

            var exitingProduct = await _productRepository.AllIncluding(c=>c.ProductCategory).AnyAsync(c => c.CategoryId == existingEntity.Id || c.ProductCategory.ParentId == existingEntity.Id);
            if (exitingProduct)
            {
                _logger.LogError("Product Category not Deleted because it is use in product");
                return ServiceResponse<bool>.Return409("Product Category not Deleted because it is use in product.");
            }

            _productCategoryRepository.Delete(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Product Category.");
                return ServiceResponse<bool>.Return500();
            }

            string contentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!string.IsNullOrWhiteSpace(existingEntity.ImageUrl)
                && File.Exists(Path.Combine(contentRootPath, _pathHelper.ProductCategoryImagePath, existingEntity.ImageUrl)))
            {
                FileData.DeleteFile(Path.Combine(contentRootPath, _pathHelper.ProductCategoryImagePath, existingEntity.ImageUrl));
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
