using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Repository.BuzonSugerencias
{
    public class BuzonSugerenciasList : List<BuzonSugerenciasDto>
    {
        public IMapper _mapper { get; set; }

        public BuzonSugerenciasList(IMapper mapper)
        {
            _mapper = mapper;
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public BuzonSugerenciasList(List<BuzonSugerenciasDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<BuzonSugerenciasList> Create(IQueryable<Data.BuzonSugerencias> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new BuzonSugerenciasList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Data.BuzonSugerencias> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<BuzonSugerenciasDto>> GetDtos(IQueryable<Data.BuzonSugerencias> source, int skip, int pageSize)
        {

            if (pageSize == 0)
            {
                var entities = await source
                                        .AsNoTracking()
                                        .ProjectTo<BuzonSugerenciasDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
                return entities;
            }
            else
            {
                var entities = await source
                                       .Skip(skip)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .ProjectTo<BuzonSugerenciasDto>(_mapper.ConfigurationProvider)
                                       .ToListAsync();

                return entities;
            }

        }
    }
}
