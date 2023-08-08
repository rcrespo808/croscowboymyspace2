using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Entities.Lookups;
using POS.Data.Resources;
using POS.Domain;
using POS.Helper;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POS.Repository
{
    public class InterestInformationRepository
        : GenericRepository<Informacioninteres, POSDbContext>, IInterestInformationRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;

        public InterestInformationRepository(IUnitOfWork<POSDbContext> uow,
            IPropertyMappingService propertyMappingService,
            IMapper mapper)
          : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }
    }
}
