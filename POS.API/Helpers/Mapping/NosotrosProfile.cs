using AutoMapper;
using POS.Data;
using POS.Data.Dto.Nosotros;
using POS.MediatR.Nosotros.Command;

namespace POS.API.Helpers.Mapping
{
    public class NosotrosProfile : Profile
    {
        public NosotrosProfile()
        {
            CreateMap<Nosotros, NosotrosDto>().ReverseMap();
            CreateMap<AddNosotrosCommand, Nosotros>().ReverseMap();
            CreateMap<AddNosotrosCommand, NosotrosDto>();
        }
    }
}