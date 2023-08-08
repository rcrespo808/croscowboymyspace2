using AutoMapper;
using POS.Data;
using POS.MediatR.Facturas.Command;
using POS.MediatR.Facturas.Commands;

namespace POS.API.Helpers.Mapping
{
    public class FacturasProfile : Profile
    {
        public FacturasProfile()
        {
            CreateMap<Facturas, FacturasDto>().ReverseMap();
            CreateMap<AddFacturasCommand, Facturas>().ReverseMap();
            CreateMap<UpdateFacturasCommand, Facturas>().ReverseMap();
            CreateMap<AddFacturasCommand, FacturasDto>();
        }
    }
}