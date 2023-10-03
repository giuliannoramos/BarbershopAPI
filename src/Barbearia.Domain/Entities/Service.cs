namespace Barbearia.Domain.Entities;

public class Service:Item
{
    public int DurationMinutes{get;set;}
    public int ServiceCategoryId{get;set;}
    public ServiceCategory ServiceCategory{get;set;} = new();
    public List<Appointment> Appointments{get;set;} = new();
    public List<Person> Persons{get;set;} = new();
}