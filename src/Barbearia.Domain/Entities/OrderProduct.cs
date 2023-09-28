namespace Barbearia.Domain.Entities;

public class OrderProduct{

    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int ItemId { get; set; }
    public Product? Product { get; set; }

}