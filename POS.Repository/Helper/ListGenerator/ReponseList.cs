using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Repository.Helper.ListGenerator
{
    public class ReponseList<V, T> : List<T> where V : class
    {
        public IMapper _mapper { get; set; }
        public ReponseList(IMapper mapper)
        {
            _mapper = mapper;
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public ReponseList(List<T> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<ReponseList<V, T>> Create(IQueryable<V> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new ReponseList<V, T>(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<V> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<T>> GetDtos(IQueryable<V> source, int skip, int pageSize)
        {
            var entities = await source
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .ProjectTo<T>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return entities;
        }
    }
}
