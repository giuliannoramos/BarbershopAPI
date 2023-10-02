namespace Barbearia.Domain.Entities;

public class RoleEmployee
{
    public int EmployeeId{get;set;}
    public Person? Employee{get;set;}
    public int RoleId{get;set;}
    public Role? Role{get;set;}
}