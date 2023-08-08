using AutoMapper;
using POS.Data;
using POS.Data.Dto.Servicio;
using POS.Helper;
using POS.MediatR.Servicios.Command;

public class ServiciosProfile : Profile
{
    public ServiciosProfile()
    {
        CreateMap<ServicioExternoDto, AddServicioCommand>();
        CreateMap<ServicioExternoDtoU, UpdateServicioCommand>();
        CreateMap<ServicioCaincoDto, AddServicioCommand>();
        CreateMap<ServicioCaincoDtoU, UpdateServicioCommand>();
        CreateMap<AddServicioCommand, Servicios>()
            .ForMember(
                servicio => servicio.UrlImage,
                servicioCommand => servicioCommand.MapFrom(e => FileManager.GetNewPathFile(e.UrlImageData, "png"))
            );
        CreateMap<UpdateServicioCommand, Servicios>()
            .ForMember(
                servicio => servicio.UrlImage,
                servicioCommand => servicioCommand.MapFrom((e, serv) => FileManager.GetUpdateFile(e.IsImageUpdated, serv.UrlImage, e.UrlImageData, "png"))
            );
    }
}
