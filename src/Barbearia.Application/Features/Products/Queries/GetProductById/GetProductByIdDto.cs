using Barbearia.Application.Models;

namespace Barbearia.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdDto
{
    public int ItemId {get;set;}
    public string Name {get;set;} = string.Empty;
    public string Description {get;set;} = string.Empty;
    public string SKU {get;set;} = string.Empty;
    public ProductCategoryDto? ProductCategory {get;set;}
    public decimal Price {get;set;}
    public int Status {get;set;}
    public int QuantityInStock{get;set;}
    public int QuantityReserved {get;set;}
}