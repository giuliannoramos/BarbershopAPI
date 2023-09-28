namespace Barbearia.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderDto
{
    public int OrderId {get;set;}
    public int Number { get; set; }
    public int Status { get; set; }
    public DateTime BuyDate { get; set; }
    public int PersonId { get; set; }
}