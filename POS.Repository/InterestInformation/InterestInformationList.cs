using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using POS.Data.Dto.InterestInformation;
using POS.Data.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Repository
{
    public class InterestInformationList : List<InterestInformationDto>
    {
        private readonly IMapper _mapper;

        public InterestInformationList(IMapper mapper)
        {
            _mapper = mapper;

        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public InterestInformationList(List<InterestInformationDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<InterestInformationList> Create(IQueryable<Informacioninteres> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new InterestInformationList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Informacioninteres> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<InterestInformationDto>> GetDtos(IQueryable<Informacioninteres> source, int skip, int pageSize)
        {

            if (pageSize == 0)
            {
                var entities = await source
                                        .AsNoTracking()
                                        .ProjectTo<InterestInformationDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
                return entities;
            }
            else
            {
                var entities = await source
                                       .Skip(skip)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .ProjectTo<InterestInformationDto>(_mapper.ConfigurationProvider)
                                       .ToListAsync();
                return entities;
            }

        }
    }
}
