using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.Beneficios.Command;

public class BeneficiosProfile : Profile
{
    public BeneficiosProfile()
    {
        CreateMap<AddBeneficioCommand, Beneficios>();
        CreateMap<BeneficioSocioDto, AddBeneficioCommand>();
        CreateMap<BeneficioSocioDto, UpdateBeneficioCommand>();
        CreateMap<UpdateBeneficioCommand, Beneficios>();
    }
}
