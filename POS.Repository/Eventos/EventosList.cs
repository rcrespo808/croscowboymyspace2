using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Repository.Eventos
{
    public class EventosList : List<EventosDto>
    {
        private readonly IMapper _mapper;

        public EventosList(IMapper mapper)
        {
            _mapper = mapper;
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public EventosList(List<EventosDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<EventosList> Create(IQueryable<Data.Entities.Lookups.Eventos> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new EventosList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Data.Entities.Lookups.Eventos> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<EventosDto>> GetDtos(IQueryable<Data.Entities.Lookups.Eventos> source, int skip, int pageSize)
        {

            if (pageSize == 0)
            {
                var entities = await source
                                        .AsNoTracking()
                                        .ProjectTo<EventosDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
                return entities;
            }
            else
            {
                var entities = await source
                                        .Skip(skip)
                                        .Take(pageSize)
                                        .AsNoTracking()
                                        .ProjectTo<EventosDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
                return entities;
            }

        }
    }
}
