using AutoMapper;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class StockHistoryProfile : Profile
{
    public StockHistoryProfile()
    {
        CreateMap<StockHistory, StockHistoryForSupplierDto>().ReverseMap();
    }
    
}