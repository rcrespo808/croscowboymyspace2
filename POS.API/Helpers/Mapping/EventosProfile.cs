using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Entities.Lookups;
using POS.Helper;
using POS.MediatR.Eventos.Command;
using System;
using System.IO;
using System.Linq;

namespace POS.API.Helpers.Mapping
{
    public class EventosProfile : Profile
    {
        public EventosProfile(PathHelper pathHelper)
        {
            CreateMap<Eventos, EventosDto>()
                .ForMember(
                    eventoDto => eventoDto.Banner1,
                    sourceEvento => sourceEvento.MapFrom(a => FileManager.GetPathFile(a.Banner1, pathHelper.EventosBanners)))
                .ForMember(
                    eventoDto => eventoDto.Banner2,
                    sourceEvento => sourceEvento.MapFrom(a => FileManager.GetPathFile(a.Banner2, pathHelper.EventosBanners)))
                .ForMember(
                    eventoDto => eventoDto.Adjunto,
                    sourceEvento => sourceEvento.MapFrom(a => FileManager.GetPathFile(a.Adjunto, pathHelper.EventosDocuments)))
                .ForMember(
                    eventoDto => eventoDto.Panelistas,
                    sourceEvento => sourceEvento.MapFrom(a => a.Panelistas.Select(p => new Customer
                    {
                        Id = p.Customer.Id,
                        CustomerName = p.Customer.CustomerName,
                        Email = p.Customer.Email,
                        ContactPerson = p.Customer.ContactPerson,
                        ImageUrl = FileManager.GetPathFile(p.Customer.Url, pathHelper.CustomerImagePath),
                        MobileNo = p.Customer.MobileNo,
                        Website = p.Customer.Website
                    })))
                .ReverseMap();

            CreateMap<AddEventosCommand, Eventos>()
                .ForMember(
                    evento => evento.Banner1,
                    eventoCommand => eventoCommand.MapFrom(e => FileManager.GetNewPathFile(e.Banner1, "png"))
                 )
                .ForMember(
                    evento => evento.Banner2,
                    eventoCommand => eventoCommand.MapFrom(e => FileManager.GetNewPathFile(e.Banner2, "png"))
                 )
                .ForMember(
                    evento => evento.Adjunto,
                    eventoCommand => eventoCommand.MapFrom(e => FileManager.GetNewPathFile(e.Adjunto, "pdf"))
                 )
                .ForMember(
                    evento => evento.Panelistas,
                    eventoCommand => eventoCommand.MapFrom(e => e.Panelistas.Select(p => new Expone { CustomerId = p.Id }))
                 )
                .ReverseMap();

            CreateMap<UpdateEventosCommand, Eventos>()
                .ForMember(
                    evento => evento.Banner1,
                    eventoCommand => eventoCommand.MapFrom((e, serv) => FileManager.GetUpdateFile(e.IsBanner1Changed, serv.Banner1, e.Banner1, "png"))
                 )
                .ForMember(
                    evento => evento.Banner2,
                    eventoCommand => eventoCommand.MapFrom((e, serv) => FileManager.GetUpdateFile(e.IsBanner2Changed, serv.Banner2, e.Banner2, "png"))
                 )
                .ForMember(
                    evento => evento.Adjunto,
                    eventoCommand => eventoCommand.MapFrom((e, serv) => FileManager.GetUpdateFile(e.IsAdjuntoChanged, serv.Adjunto, e.Adjunto, "pdf"))
                 )
                .ForMember(
                    evento => evento.Panelistas,
                    eventoCommand => eventoCommand.MapFrom((e, serv) => serv.Panelistas)
                 )
                .ReverseMap();

            
        }
    }
}
