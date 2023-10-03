namespace Barbearia.Domain.Entities;

public class Appointment
{
    public int AppointmentId{get; set;}
    public int ScheduleId{get; set;}
    public Schedule? Schedule{get; set;}
    public int CustomerId{get; set;}
    public Person? Person{get;set;}
    public DateTime StartDate{get; set;}
    public DateTime FinishDate{get; set;}
    public int Status{get; set;}
    public DateTime StartServiceDate{get;set;}
    public DateTime FinishServiceDate{get;set;}
    public DateTime ConfirmedDate{get;set;}
    public DateTime CancellationDate{get;set;}
    public List<Service> Services{get;set;} = new();
    public List<Order> Orders {get;set;} = new();



    private void CheckOrder()
    {
        if(Orders.Count>1) throw new Exception("Appointment só pode estar em uma order");
    }

    public void ValidateAppointment()
    {
        CheckOrder();
    }

}