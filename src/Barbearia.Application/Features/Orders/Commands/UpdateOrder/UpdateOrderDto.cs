namespace Barbearia.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderDto
{    
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public int Number { get; set; }
    public int Status { get; set; }
    public DateTime BuyDate { get; set; }
}