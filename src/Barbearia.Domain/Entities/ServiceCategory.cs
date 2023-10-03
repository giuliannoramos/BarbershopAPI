namespace Barbearia.Domain.Entities;

public class ServiceCategory
{
    public int ServiceCategoryId{get;set;}
    public string Name{get; set;} = string.Empty;
    public List<Service>Services{get;set;} = new();
    public List<Role> Roles {get;set;} = new();

    private void CheckName()
    {
        if(Name == null || Name == string.Empty) throw new Exception("Category must have a name");
    }

    public void ValidateServiceCategory()
    {
        CheckName();
    }
}