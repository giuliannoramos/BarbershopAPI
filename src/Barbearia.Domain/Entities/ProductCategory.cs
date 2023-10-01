namespace Barbearia.Domain.Entities;

public class ProductCategory{
    public int ProductCategoryId {get;set;}
    public string Name {get;set;} = string.Empty;
    public List<Product> Product { get; set; } = new();
}