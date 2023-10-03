namespace Barbearia.Domain.Entities;

public class Person
{
    public int PersonId {get;set;}
    public string Name{get;set;} = string.Empty;
    public DateOnly BirthDate{get;set;}
    public int Gender { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Cnpj {get;set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public TypeStatus Status { get; set; }
    public List<Address> Addresses {get; set;} = new();
    public List<Telephone> Telephones {get; set;} = new();
    public List<Order> Orders { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public List <StockHistory> StockHistories { get; set; } = new();
    public List<WorkingDay> WorkingDays{get;set;} = new();
    public List<Role> Roles{get;set;} = new();
    public List<RoleEmployee> RoleEmployees{get; set;} = new();
    public List<Appointment> Appointments{get;set;} = new();
    public List<Service> Services{get;set;} = new();

    public enum TypeStatus
    {
        NoStatus,
        Active,
        Inactive        
    }

}