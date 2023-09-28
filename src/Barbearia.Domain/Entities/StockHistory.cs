namespace Barbearia.Domain.Entities;
public class StockHistory{
    public int StockHistoryId {get;set;}
    public int Operation {get;set;}
    public float CurrentPrice {get;set;}
    public int Amount {get;set;}
    public DateTime Timestamp {get;set;}
    public int LastStockQuantity {get;set;}
    public int PersonId {get;set;}
    public Suplier? Suplier {get;set;}
    public int ItemId {get;set;}
    public Product? Product {get;set;}
    public int OrderId {get;set;}
    public Order? Order {get;set;}
}