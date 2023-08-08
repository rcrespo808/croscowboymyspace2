using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Repository.Facturas
{
    public class FacturasList : List<FacturasDto>
    {
        public IMapper _mapper { get; set; }

        public FacturasList(IMapper mapper)
        {
            _mapper = mapper;
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public FacturasList(List<FacturasDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<FacturasList> Create(IQueryable<Data.Facturas> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new FacturasList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Data.Facturas> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<FacturasDto>> GetDtos(IQueryable<Data.Facturas> source, int skip, int pageSize)
        {

            if (pageSize == 0)
            {
                var entities = await source
                                        .AsNoTracking()
                                        .ProjectTo<FacturasDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
                return entities;
            }
            else
            {
                var entities = await source
                                       .Skip(skip)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .ProjectTo<FacturasDto>(_mapper.ConfigurationProvider)
                                       .ToListAsync();

                return entities;
            }

        }
    }
}
