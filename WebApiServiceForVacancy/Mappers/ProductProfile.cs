using AutoMapper;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.CQRS.Models.Commands.ProductCommands;
using WebApiServiceForVacancy.Data.Entities;
using WebApiServiceForVacancy.Models.Requests;

namespace WebApiServiceForVacancy.Mappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateNewProductRequest, CreateNewProductDto>()
            .ForMember(dto => dto.Name, opt => opt.MapFrom(request => request.Name))
            .ForMember(dto => dto.IsAvailable, opt => opt.MapFrom(request => request.IsAvailable));

        CreateMap<CreateProductCommand, Product>()
            .ForMember(entity => entity.Name, opt => opt.MapFrom(command => command.Name))
            .ForMember(entity => entity.IsAvailable, opt => opt.MapFrom(command => command.IsAvailable));

        CreateMap<Product, ProductDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
            .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Name))
            .ForMember(dto => dto.IsAvailable, opt => opt.MapFrom(entity => entity.IsAvailable));

        CreateMap<ProductDto, EditProductCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.Id))
            .ForMember(command => command.Name, opt => opt.MapFrom(dto => dto.Name))
            .ForMember(command => command.IsAvailable, opt => opt.MapFrom(dto => dto.IsAvailable));

        CreateMap<EditProductCommand, Product>()
            .ForMember(entity => entity.Id, opt => opt.MapFrom(command => command.Id))
            .ForMember(entity => entity.Name, opt => opt.MapFrom(command => command.Name))
            .ForMember(entity => entity.IsAvailable, opt => opt.MapFrom(command => command.IsAvailable));
    }
}