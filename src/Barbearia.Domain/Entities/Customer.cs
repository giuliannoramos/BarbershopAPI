namespace Barbearia.Domain.Entities;

public class Customer : Person
{
    public void ValidateCustomerTelephone()
    {
        if (Telephones.Count == 0)
        {
            throw new Exception("Um cliente deve conter pelo menos um telefone.");
        }
        if (Telephones.Count > 1)
        {
            throw new Exception("Um cliente só pode ter um telefone principal.");
        }
    }

    public void ValidateCustomerAddress()
    {        
        if (Addresses.Count > 1)
        {
            throw new Exception("Um cliente só pode ter um endereço cadastrado.");
        }
    }

    public void CantHaveCNPJ(){
        if(Cnpj !=null){
            throw new Exception("Cliente não pode ter cnpj cadastrado");
        }
    }
}