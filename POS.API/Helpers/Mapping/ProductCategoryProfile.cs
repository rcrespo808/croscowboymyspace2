using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Category.Commands;
using System;
using System.IO;

namespace POS.API.Helpers.Mapping
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile(PathHelper pathHelper)
        {
            CreateMap<ProductCategory, ProductCategoryDto>()
                .ForMember(
                    evento => evento.ImageUrl,
                    eventoCommand => eventoCommand.MapFrom(e => FileManager.GetPathFile(e.ImageUrl, pathHelper.ProductCategoryImagePath))
                 )
                .ReverseMap();
            CreateMap<AddProductCategoryCommand, ProductCategory>()
                .ForMember(
                    evento => evento.ImageUrl,
                    eventoCommand => eventoCommand.MapFrom(e => FileManager.GetNewPathFile(e.ImageData, "png"))
                 );
            CreateMap<UpdateProductCategoryCommand, ProductCategory>()
                .ForMember(
                    evento => evento.ImageUrl,
                    eventoCommand => eventoCommand.MapFrom((e, evento) => FileManager.GetUpdateFile(e.IsImageChanged, evento.ImageUrl, e.ImageData, "png"))
                 );
        }
    }
}
