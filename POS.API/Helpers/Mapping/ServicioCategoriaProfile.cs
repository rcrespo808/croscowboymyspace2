using AutoMapper;
using POS.Data;
using POS.Helper;
using POS.MediatR.ServicioCategoria.Command;

public class ServicioCategoriaProfile : Profile
{
    public ServicioCategoriaProfile()
    {
        CreateMap<AddServicioCategoriaCommand, ServiciosCategorias>()
            .ForMember(
                servicio => servicio.UrlImage,
                servicioCommand => servicioCommand.MapFrom(e => FileManager.GetNewPathFile(e.UrlImageData, "png"))
            );
        CreateMap<UpdateServicioCategoriaCommand, ServiciosCategorias>()
            .ForMember(
                servicio => servicio.UrlImage,
                servicioCommand => servicioCommand.MapFrom((e, ev) => FileManager.GetUpdateFile(e.IsImageUpdated, ev.UrlImage, e.UrlImageData, "png"))
            );
    }
}
