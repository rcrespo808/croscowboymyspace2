using AutoMapper;
using POS.Data;
using POS.Data.Dto;
using POS.Helper;
using POS.MediatR.Product.Command;
using System.Diagnostics.Tracing;
using System.IO;

namespace POS.API.Helpers.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile(PathHelper pathHelper)
        {
            CreateMap<Product, ProductDto>()
                .ForMember(
                    productDto => productDto.ProductUrl,
                    product => product.MapFrom(e => FileManager.GetPathFile(e.ProductUrl, pathHelper.ProductImagePath))
                 )
                .ForMember(
                    productDto => productDto.CategoryName,
                    product => product.MapFrom(e => e.ProductCategory.Name)
                 )
                .ForMember(
                    productDto => productDto.UnitName,
                    product => product.MapFrom(e => e.Unit.Name)
                 )
                .ForMember(
                    productDto => productDto.BrandName,
                    product => product.MapFrom(e => e.Brand.Name ?? "")
                 )
                .ReverseMap();
            CreateMap<Product, ProductUsuarioDto>()
                .ForMember(
                    productDto => productDto.ProductUrl,
                    product => product.MapFrom(e => FileManager.GetPathFile(e.ProductUrl, pathHelper.ProductImagePath))
                 )
                .ForMember(
                    productDto => productDto.CategoryName,
                    product => product.MapFrom(e => e.ProductCategory.Name)
                 )
                .ForMember(
                    productDto => productDto.UnitName,
                    product => product.MapFrom(e => e.Unit.Name)
                 )
                .ReverseMap();
            CreateMap<ProductTax, ProductTaxDto>().ReverseMap();
            CreateMap<AddProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}
