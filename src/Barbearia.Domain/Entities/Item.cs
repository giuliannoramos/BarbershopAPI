namespace Barbearia.Domain.Entities;

public abstract class Item {
    public int ItemId {get;set;}
    public string Name {get;set;} = string.Empty;
    public decimal Price{get;set;}
    public string Description {get;set;} = string.Empty;
}