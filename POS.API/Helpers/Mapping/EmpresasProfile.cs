using AutoMapper;
using POS.Data.Dto.Empresas;
using POS.Data.Entities.Empresas;
using POS.Helper;
using POS.MediatR.Empresas.Command;
using POS.MediatR.Empresas.Commands;

namespace POS.API.Helpers.Mapping
{
    public class EmpresasProfile : Profile
    {
        public EmpresasProfile(PathHelper pathHelper)
        {
            CreateMap<Empresas, EmpresasDto>()
                .ForMember(
                    empresas => empresas.UrlLogo,
                    empresasDto => empresasDto.MapFrom(e => FileManager.GetPathFile(e.UrlLogo, pathHelper.EmpresasImagePath))
                );
            CreateMap<AddEmpresasCommand, Empresas>()
                .ForMember(
                    empresas => empresas.UrlLogo,
                    empresasCommand => empresasCommand.MapFrom(e => FileManager.GetNewPathFile(e.UrlLogoData, "png"))
                );
            CreateMap<UpdateEmpresasCommand, Empresas>()
                .ForMember(
                    empresas => empresas.UrlLogo,
                    empresasCommand => empresasCommand.MapFrom((e, serv) => FileManager.GetUpdateFile(e.IsLogoChanged, serv.UrlLogo, e.UrlLogoData, "png"))
                );
        }
    }
}