namespace Barbearia.Domain.Entities;

public class StockHistorySupplier : StockHistory
{
    public int PersonId { get; set; }
    public Person? Supplier { get; set; }

    private void CheckPersonId()
    {
        if (PersonId <= 0)
        {
            throw new ArgumentException("PersonId deve ser maior que zero.");
        }
    }

    private void CheckOperation()
    {
        if (Operation < 0)
        {
            throw new ArgumentException("Operação inválida. A operação deve ser maior que zero.");
        }
    }

    private void CheckCurrentPrice()
    {
        if (CurrentPrice < 0)
        {
            throw new ArgumentException("Preço atual inválido. O preço atual deve ser maior que zero.");
        }
    }

    private void CheckAmount()
    {
        if (Amount < 0)
        {
            throw new ArgumentException("Quantidade inválida. A quantidade deve ser maior que zero.");
        }
    }

    private void CheckTimestamp()
    {
        if (Timestamp > DateTime.UtcNow)
        {
            throw new ArgumentException("O registro de histórico não pode estar no futuro.");
        }
    }


    public void ValidateStockHistorySupplier()
    {
        CheckPersonId();
        CheckOperation();
        CheckCurrentPrice();
        CheckAmount();
        CheckTimestamp();
    }
}