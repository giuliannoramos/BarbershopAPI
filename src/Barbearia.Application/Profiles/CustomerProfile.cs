using AutoMapper;
using Barbearia.Application.Features.Customers.Commands;
using Barbearia.Application.Features.Customers.Queries.GetAllCustomers;
using Barbearia.Application.Features.Customers.Queries.GetCustomerById;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, GetCustomerByIdDto>();
        CreateMap<Customer, GetAllCustomersDto>();
        CreateMap<CreateCustomerCommand, Customer>();        
        CreateMap<Telephone, List<Telephone>>();
        CreateMap<TelephoneDto, Telephone>().ReverseMap();
        CreateMap<AddressDto, Address>().ReverseMap();
        CreateMap<Customer, CreateCustomerDto>();  
        CreateMap<CustomerForCreationDto, Customer>();     
    }
}