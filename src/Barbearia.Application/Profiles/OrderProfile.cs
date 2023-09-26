using AutoMapper;
using Barbearia.Application.Features.Orders.Commands.UpdateOrder;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<UpdateOrderCommand, Order>(); 
        CreateMap<Order,UpdateOrderDto>();       
    }
}