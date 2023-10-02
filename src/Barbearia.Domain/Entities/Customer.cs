namespace Barbearia.Domain.Entities;

public class Customer : Person
{
    private void CheckCustomerTelephone()
    {
        if (Telephones.Count == 0)
        {
            throw new ArgumentException("Um cliente deve conter pelo menos um telefone.");
        }
        if (Telephones.Count > 1)
        {
            throw new ArgumentException("Um cliente só pode ter um telefone principal.");
        }
    }

    private void CheckCustomerAddress()
    {
        if (Addresses.Count > 1)
        {
            throw new ArgumentException("Um cliente só pode ter um endereço cadastrado.");
        }
    }

    private void CheckCnpj()
    {
        if (!string.IsNullOrEmpty(Cnpj))
        {
            throw new ArgumentException("Cliente não pode ter CNPJ cadastrado.");
        }
    }

    private void CheckStatus()
    {
        if(Status !=0)
        {
            throw new ArgumentException("Cliente não pode ter status");
        }
    }

    public void ValidateCustomer()
    {
        CheckCustomerTelephone();
        CheckCustomerAddress();
        CheckCnpj();
        CheckStatus();
    }
}