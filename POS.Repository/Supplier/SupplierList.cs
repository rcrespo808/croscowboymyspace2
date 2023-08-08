﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Data.Dto;
using POS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Repository
{
    public class SupplierList : List<SupplierDto>
    {
        public IMapper _mapper { get; set; }
        public PathHelper _pathHelper { get; set; }

        public SupplierList(IMapper mapper, PathHelper pathHelper)
        {
            _mapper = mapper;
            _pathHelper = pathHelper;
        }
        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public SupplierList(List<SupplierDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<SupplierList> Create(IQueryable<Supplier> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new SupplierList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Supplier> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<SupplierDto>> GetDtos(IQueryable<Supplier> source, int skip, int pageSize)
        {
            var entities = await source
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return entities;
        }
    }
}
