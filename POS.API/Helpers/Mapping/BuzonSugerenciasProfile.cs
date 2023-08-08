using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.BuzonSugerencias.Command;
using POS.MediatR.BuzonSugerencias.Commands;

namespace POS.API.Helpers.Mapping
{
    public class BuzonSugerenciasProfile : Profile
    {
        public BuzonSugerenciasProfile()
        {
            CreateMap<BuzonSugerencias, BuzonSugerenciasDto>();
            CreateMap<AddBuzonSugerenciasCommand, BuzonSugerencias>();
            CreateMap<UpdateBuzonSugerenciasCommand, BuzonSugerencias>();
        }
    }
}