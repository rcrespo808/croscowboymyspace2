using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Publicidad.Command;
using POS.MediatR.Publicidad.Commands;

namespace POS.API.Helpers.Mapping
{
    public class PublicidadProfile : Profile
    {
        public PublicidadProfile(PathHelper pathHelper)
        {
            CreateMap<Publicidad, PublicidadDto>()
                .ForMember(
                    publicidad => publicidad.UrlBanner,
                    publicidadDto => publicidadDto.MapFrom(e => FileManager.GetPathFile(e.UrlBanner, pathHelper.PublicidadImagePath))
                );
            CreateMap<AddPublicidadCommand, Publicidad>()
                .ForMember(
                    publicidad => publicidad.UrlBanner,
                    publicidadCommand => publicidadCommand.MapFrom(e => FileManager.GetNewPathFile(e.UrlBannerData, "png"))
                );
            CreateMap<UpdatePublicidadCommand, Publicidad>()
                .ForMember(
                    publicidad => publicidad.UrlBanner,
                    publicidadCommand => publicidadCommand.MapFrom((e, serv) => FileManager.GetUpdateFile(e.IsBannerChanged, serv.UrlBanner, e.UrlBannerData, "png"))
                );
        }
    }
}