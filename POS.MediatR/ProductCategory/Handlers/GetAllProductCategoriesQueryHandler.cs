using AutoMapper;
using POS.Data.Dto;
using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System;
using POS.Helper.Constants;

namespace POS.MediatR.Handler
{
    public class GetAllProductCategoriesQueryHandler : IRequestHandler<GetAllProductCategoriesQuery, List<ProductCategoryDto>>
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllProductCategoriesQueryHandler(IProductCategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductCategoryDto>> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            Guid valueId;
            var servicioId = !Constants.Servicios.TryGetValue(request.ProductCategoryCainco ?? string.Empty,out valueId) ? Guid.Empty : valueId;
            var parentId = request.Id is null ? servicioId == Guid.Empty ? null : servicioId : request.Id;
            var categories = await _categoryRepository.All
                .Where(c => c.ParentId == parentId)
                .OrderBy(c => c.Name)
                .ProjectTo<ProductCategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return categories;
        }
    }
}
