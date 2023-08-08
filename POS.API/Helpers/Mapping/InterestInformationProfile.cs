using AutoMapper;
using POS.Data.Dto.InterestInformation;
using POS.Data.Entities.Lookups;
using POS.Helper;
using POS.MediatR.InterestInformation.Command;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POS.API.Helpers.Mapping
{
    public class InterestInformationProfile : Profile
    {
        public InterestInformationProfile(PathHelper pathHelper)
        {
            CreateMap<Informacioninteres, InterestInformationDto>()
                .ForMember(
                    informacionInteres => informacionInteres.Logo,
                    informacionInteresDto => informacionInteresDto.MapFrom(e => FileManager.GetPathFile(e.Logo, pathHelper.InterestInformationLogos))
                )
                .ForMember(
                    informacionInteres => informacionInteres.Documento,
                    informacionInteresDto => informacionInteresDto.MapFrom(e => FileManager.GetPathFile(e.Documento, pathHelper.InterestInformationDocuments))
                )
                .ForMember(
                    informacionInteres => informacionInteres.Etiquetas,
                    informacionInteresCommand => informacionInteresCommand.MapFrom(e => string.IsNullOrEmpty(e.Etiquetas) ? new List<string>{} : e.Etiquetas.Split(",", StringSplitOptions.None).ToList() )
                );
            CreateMap<AddInterestInformationCommand, Informacioninteres>()
                .ForMember(
                    informacionInteres => informacionInteres.Logo,
                    informacionInteresCommand => informacionInteresCommand.MapFrom(e => FileManager.GetNewPathFile(e.Logo, "png"))
                )
                .ForMember(
                    informacionInteres => informacionInteres.Documento,
                    informacionInteresCommand => informacionInteresCommand.MapFrom(e => FileManager.GetNewPathFile(e.Documento, "pdf"))
                )
                .ForMember(
                    informacionInteres => informacionInteres.Etiquetas,
                    informacionInteresCommand => informacionInteresCommand.MapFrom(e => string.Join(",", e.Etiquetas.ToArray()))
                );
            CreateMap<UpdateInterestInformationCommand, Informacioninteres>()
                .ForMember(
                    informacionInteres => informacionInteres.Logo,
                    informacionInteresCommand => informacionInteresCommand.MapFrom((e, serv) => FileManager.GetUpdateFile(e.IsLogoChanged, serv.Logo, e.Logo, "png"))
                )
                .ForMember(
                    informacionInteres => informacionInteres.Documento,
                    informacionInteresCommand => informacionInteresCommand.MapFrom((e, serv) => FileManager.GetUpdateFile(e.IsDocumentChanged, serv.Documento, e.Documento, "pdf"))
                )
                .ForMember(
                    informacionInteres => informacionInteres.Etiquetas,
                    informacionInteresCommand => informacionInteresCommand.MapFrom(e => string.Join(",", e.Etiquetas.ToArray()))
                );
        }
    }
}