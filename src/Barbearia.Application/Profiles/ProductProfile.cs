using AutoMapper;
using Barbearia.Application.Features.ProductsCollection;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, GetProductsCollectionDto>();    
        CreateMap<Product, ProductForSupplierDto>();    
    }
}