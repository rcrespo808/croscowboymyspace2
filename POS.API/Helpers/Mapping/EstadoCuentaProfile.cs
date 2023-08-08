using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.EstadoCuenta.Command;
using POS.MediatR.EstadoCuenta.Commands;

namespace POS.API.Helpers.Mapping
{
    public class EstadoCuentaProfile : Profile
    {
        public EstadoCuentaProfile()
        {
            CreateMap<EstadoCuenta, EstadoCuentaDto>().ReverseMap();
            CreateMap<AddEstadoCuentaCommand, EstadoCuenta>();
            CreateMap<UpdateEstadoCuentaCommand, EstadoCuenta>();
        }
    }
}