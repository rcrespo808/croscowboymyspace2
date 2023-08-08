using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.Helper;
using System.IO;

namespace POS.API.Helpers.Mapping
{
    public class AsistenciaProfile : Profile
    {
        public AsistenciaProfile(PathHelper pathHelper)
        {
            CreateMap<Asistencia, UserAttendeeDto>()
                .ForMember(
                    userDto => userDto.Id,
                    userCommand => userCommand.MapFrom(e => e.User.Id)
                 )
                .ForMember(
                    userDto => userDto.UserName,
                    userCommand => userCommand.MapFrom(e => e.User.UserName)
                 )
                .ForMember(
                    userDto => userDto.Email,
                    userCommand => userCommand.MapFrom(e => e.User.Email)
                 )
                .ForMember(
                    userDto => userDto.FirstName,
                    userCommand => userCommand.MapFrom(e => e.User.FirstName)
                 )
                .ForMember(
                    userDto => userDto.LastName,
                    userCommand => userCommand.MapFrom(e => e.User.LastName)
                 )
                .ForMember(
                    userDto => userDto.PhoneNumber,
                    userCommand => userCommand.MapFrom(e => e.User.PhoneNumber)
                 )
                .ForMember(
                    userDto => userDto.Address,
                    userCommand => userCommand.MapFrom(e => e.User.Address)
                 )
                .ForMember(
                    userDto => userDto.ProfilePhoto,
                    userCommand => userCommand.MapFrom(e => string.IsNullOrWhiteSpace(e.User.ProfilePhoto) ? "" : Path.Combine(pathHelper.UserProfilePath, e.User.ProfilePhoto))
                 )
                .ForMember(
                    userDto => userDto.Provider,
                    userCommand => userCommand.MapFrom(e => e.User.Provider)
                 );
        }
    }
}
