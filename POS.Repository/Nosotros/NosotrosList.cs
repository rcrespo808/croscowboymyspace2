using Microsoft.EntityFrameworkCore;
using POS.Data.Dto.Nosotros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Repository
{
    public class NosotrosList : List<NosotrosDto>
    {
        public NosotrosList()
        {
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public NosotrosList(List<NosotrosDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<NosotrosList> Create(IQueryable<Data.Nosotros> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new NosotrosList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Data.Nosotros> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<NosotrosDto>> GetDtos(IQueryable<Data.Nosotros> source, int skip, int pageSize)
        {

            if (pageSize == 0)
            {
                var entities = await source
            .AsNoTracking()
            .Select(c => new NosotrosDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Location = c.Location,
                ImageUrl = c.ImageUrl,
                CreatedDate = c.CreatedDate
            }).ToListAsync();
                return entities;
            }
            else
            {
                var entities = await source
               .Skip(skip)
               .Take(pageSize)
               .AsNoTracking()
               .Select(c => new NosotrosDto
               {
                   Id = c.Id,
                   Title = c.Title,
                   Description = c.Description,
                   Location = c.Location,
                   ImageUrl = c.ImageUrl,
                   CreatedDate = c.CreatedDate
               }).ToListAsync();
                return entities;
            }

        }
    }
}
