using AutoMapper;
using Barbearia.Application.Features.Orders.Commands.UpdateOrder;
using Barbearia.Application.Features.OrdersCollection;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<UpdateOrderCommand, Order>(); 
        CreateMap<Order,UpdateOrderDto>();
        
        CreateMap<Order, GetOrdersCollectionDto>();
        CreateMap<PersonDto, Person>().ReverseMap();     
    }
}