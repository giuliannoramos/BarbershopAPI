namespace Barbearia.Domain.Entities;

public class ServiceCategory
{
    public int ServiceCategoryId{get;set;}
    public string Name{get; set;} = string.Empty;
    public List<Service>Services{get;set;} = new();
    public List<Role> Roles {get;set;} = new();
}