namespace Barbearia.Domain.Entities;

public abstract class Item {
    public int ItemId {get;set;}
    public string Name {get;set;} = string.Empty;
    public string Description {get;set;} = string.Empty;
}