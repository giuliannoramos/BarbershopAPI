namespace Barbearia.Domain.Entities;

public class Product : Item{// não entendi se o product tem o mesmo id do item ou se botaram errado
    public string Brand {get;set;} = string.Empty;
    public string SKU {get;set;} = string.Empty;//verificar se ja tem um sku no sistema ja pra não repetir
    public int QuantityInStock{get;set;}
    public int QuantityReserved {get;set;}
    public List<StockHistory> StockHistories { get; set; } = new();
    public int ProductCategoryId { get; set; }
    public ProductCategory? ProductCategory { get; set; }
    public int PersonId { get; set; }
    public Suplier? Suplier { get; set; }
    public List<Order> Orders { get; set; } = new();
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    private void CheckBrand()
        {
            if (string.IsNullOrWhiteSpace(Brand))
            {
                throw new ArgumentException("A marca não pode estar vazia ou conter apenas espaços em branco.");
            }
        }

        private void CheckSKU()
        {            
            if (string.IsNullOrWhiteSpace(SKU))
            {
                throw new ArgumentException("O SKU não pode estar vazio.");
            }
        }

        private void CheckQuantityInStock()
        {
            if (QuantityInStock < 0)
            {
                throw new ArgumentException("Quantidade em estoque inválida. Deve ser maior ou igual a zero.");
            }
        }

        private void CheckQuantityReserved()
        {
            if (QuantityReserved < 0)
            {
                throw new ArgumentException("Quantidade reservada inválida. Deve ser maior ou igual a zero.");
            }
        }

        public void ValidateProduct()
        {
            CheckBrand();
            CheckSKU();
            CheckQuantityInStock();
            CheckQuantityReserved();
        }
}