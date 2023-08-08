using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using System.Collections.Generic;
using System;
using System.Linq;

namespace POS.API.Helpers
{
    /// <summary>
    /// Supplier Mapper for Autommaper
    /// </summary>
    public class SupplierProfile : Profile
    {
        /// <summary>
        /// Supplier Mapper for Autommaper
        /// </summary>
        public SupplierProfile(PathHelper pathHelper)
        {
            CreateMap<SupplierAddressDto, SupplierAddress>()
                .ReverseMap();

            CreateMap<Supplier, SupplierDto>()
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description == null ? string.Empty : src.Description))
                .ForMember(
                    supplier => supplier.LogoUrl,
                    supplierCommand => supplierCommand.MapFrom(e => FileManager.GetPathFile(e.LogoUrl, pathHelper.SupplierImagePath))
                 )
                .ForMember(
                    supplier => supplier.BannerUrl,
                    supplierCommand => supplierCommand.MapFrom(e => FileManager.GetPathFile(e.BannerUrl, pathHelper.SupplierImagePath))
                 )
                .ForMember(
                    supplier => supplier.ContactPersonList,
                    supplierCommand => supplierCommand.MapFrom(e => string.IsNullOrEmpty(e.ContactPersonList) ? new List<string> { } : e.ContactPersonList.Split(",", StringSplitOptions.None).ToList())
                ); ;

            CreateMap<AddSupplierCommand, Supplier>()
                .ForMember(
                    supplier => supplier.LogoUrl,
                    supplierCommand => supplierCommand.MapFrom(e => FileManager.GetNewPathFile(e.LogoUrlData, "png"))
                 )
                .ForMember(
                    supplier => supplier.BannerUrl,
                    supplierCommand => supplierCommand.MapFrom(e => FileManager.GetNewPathFile(e.BannerUrlData, "png"))
                 )
                .ForMember(
                    supplier => supplier.ContactPersonList,
                    supplierCommand => supplierCommand.MapFrom(e => string.Join(",", e.ContactPersonList.ToArray()))
                );

            CreateMap<UpdateSupplierCommand, Supplier>()
                .ForMember(
                    supplier => supplier.LogoUrl,
                    supplierCommand => supplierCommand.MapFrom((e, serv) => FileManager.GetUpdateFile(e.IsLogoUpdated, serv.LogoUrl, e.LogoUrlData, "png"))
                 )
                .ForMember(
                    supplier => supplier.BannerUrl,
                    supplierCommand => supplierCommand.MapFrom((e, serv) => FileManager.GetUpdateFile(e.IsBannerUpdated, serv.BannerUrl, e.BannerUrlData, "png"))
                 )
                .ForMember(
                    supplier => supplier.ContactPersonList,
                    supplierCommand => supplierCommand.MapFrom(e => string.Join(",", e.ContactPersonList.ToArray()))
                );
        }
    }
}
