using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.Certificados.Command;
using POS.MediatR.Certificados.Commands;

namespace POS.API.Helpers.Mapping
{
    public class CertificadosProfile : Profile
    {
        public CertificadosProfile()
        {
            CreateMap<Certificados, CertificadosDto>();
            CreateMap<AddCertificadosCommand, Certificados>();
            CreateMap<UpdateCertificadosCommand, Certificados>();
        }
    }
}