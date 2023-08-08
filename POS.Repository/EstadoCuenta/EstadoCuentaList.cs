using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Repository.EstadoCuenta
{
    public class EstadoCuentaList : List<EstadoCuentaDto>
    {
        public IMapper _mapper { get; set; }

        public EstadoCuentaList(IMapper mapper)
        {
            _mapper = mapper;
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public EstadoCuentaList(List<EstadoCuentaDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<EstadoCuentaList> Create(IQueryable<Data.EstadoCuenta> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new EstadoCuentaList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Data.EstadoCuenta> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<EstadoCuentaDto>> GetDtos(IQueryable<Data.EstadoCuenta> source, int skip, int pageSize)
        {

            if (pageSize == 0)
            {
                var entities = await source
                                        .AsNoTracking()
                                        .ProjectTo<EstadoCuentaDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
                return entities;
            }
            else
            {
                var entities = await source
                                       .Skip(skip)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .ProjectTo<EstadoCuentaDto>(_mapper.ConfigurationProvider)
                                       .ToListAsync();

                return entities;
            }

        }
    }
}
