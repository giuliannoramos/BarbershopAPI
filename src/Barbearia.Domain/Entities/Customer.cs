namespace Barbearia.Domain.Entities;

public class Customer : Person
{


    //deve ser refatorado  pois agora telephone e addresses são listas já que agora é uma relação de um para muitos

    // public void ValidateCustomerTelephoneNotNull()
    // {
    //     // Verifica se não há telefone cadastrado
    //     if (Telephone == null)
    //     {
    //         throw new Exception("Um cliente deve conter um telefone.");
    //     }
    // }

    // public void ValidateCustomerTelephone()
    // {
    //     // Verifica se já existe um telefone cadastrado
    //     if (Telephone != null)
    //     {
    //         throw new Exception("Um cliente só pode ter um telefone cadastrado.");
    //     }
    // }

    // public void ValidateCustomerAddress()
    // {
    //     // Verifica se já existe um endereço cadastrado
    //     if (Address != null)
    //     {
    //         throw new Exception("Um cliente só pode ter um endereço cadastrado.");
    //     }
    // }

    public void CantHaveCNPJ(){
        if(Cnpj !=null){
            throw new Exception("Cliente não pode ter cnpj cadastrado");
        }
    }
}