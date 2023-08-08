using AutoMapper;
using POS.Data;
using POS.Helper;
using POS.MediatR.BeneficioCategoria.Command;

public class BeneficioCategoriaProfile : Profile
{
    public BeneficioCategoriaProfile()
    {
        CreateMap<AddBeneficioCategoriaCommand, BeneficiosCategorias>()
            .ForMember(
                beneficio => beneficio.UrlBanner,
                beneficioCommand => beneficioCommand.MapFrom(e => FileManager.GetNewPathFile(e.UrlBannerData, "png"))
            );
        CreateMap<UpdateBeneficioCategoriaCommand, BeneficiosCategorias>()
            .ForMember(
                beneficio => beneficio.UrlBanner,
                beneficioCommand => beneficioCommand.MapFrom((e, ev) => FileManager.GetUpdateFile(e.IsBannerUpdated, ev.UrlBanner, e.UrlBannerData, "png"))
            );
    }
}
