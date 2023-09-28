namespace Barbearia.Domain.Entities;

public class ProductCategory{
    public int ProductCategoryId {get;set;}
    public string Name {get;set;} = string.Empty;
    public Product? Product { get; set; }
}