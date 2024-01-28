using AutoMapper;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.CQRS.Models.Commands.OrderCommands;
using WebApiServiceForVacancy.Data.Entities;
using WebApiServiceForVacancy.Models.Requests;
using WebApiServiceForVacancy.Models.Responses;

namespace WebApiServiceForVacancy.Mappers;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateNewOrderRequest, CreateNewOrderDto>()
            .ForMember(dto => dto.Number, opt => opt.MapFrom(request => request.Number))
            .ForMember(dto => dto.CreateDateTime, opt => opt.MapFrom(request => DateTime.UtcNow))
            .ForMember(dto => dto.CustomerId, opt => opt.MapFrom(request => request.CustomerId))
            .ForMember(dto => dto.ProductIds, opt => opt.MapFrom(request => request.ProductIds));

        CreateMap<CreateNewOrderCommand, Order>()
            .ForMember(entity => entity.Number, opt => opt.MapFrom(command => command.Number))
            .ForMember(entity => entity.CreateDateTime, opt => opt.MapFrom(command => command.CreateDateTime))
            .ForMember(entity => entity.CustomerId, opt => opt.MapFrom(command => command.CustomerId));

        CreateMap<Order, OrderDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
            .ForMember(dto => dto.Number, opt => opt.MapFrom(entity => entity.Number))
            .ForMember(dto => dto.CreateDateTime, opt => opt.MapFrom(entity => entity.CreateDateTime))
            .ForMember(dto => dto.CustomerId, opt => opt.MapFrom(entity => entity.CustomerId))
            .ForMember(dto => dto.OrderProducts, opt => opt.MapFrom(entity => entity.OrderProducts));

        CreateMap<OrderDto, GetOrderByCustomerIdResponce>()
            .ForMember(responce => responce.Id, opt => opt.MapFrom(dto => dto.Id))
            .ForMember(responce => responce.Number, opt => opt.MapFrom(dto => dto.Number))
            .ForMember(responce => responce.CreateDateTime, opt => opt.MapFrom(dto => dto.CreateDateTime))
            .ForMember(responce => responce.CustomerId, opt => opt.MapFrom(dto => dto.CustomerId));
    }
}