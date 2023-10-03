namespace Barbearia.Domain.Entities;

public class AppointmentService
{
    public int AppointmentServiceId{get;set;}
    public int ServiceId{get;set;}
    public int EmployeeId{get;set;}
    public int AppointmentId{get;set;}
    public string Name{get;set;} = string.Empty;
    public int DurationMinutes{get;set;}
    public Decimal CurrentPrice{get;set;}


    //entidade associativa
}

