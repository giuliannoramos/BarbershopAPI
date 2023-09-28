namespace Barbearia.Domain.Entities;

public class Suplier : Person
{
    // public void ValidateCnpj()
    // {
    //     int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    //     int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    //     int soma;
    //     int resto;
    //     string digito;
    //     string tempCnpj;
    //     Cnpj = Cnpj.Trim();
    //     Cnpj = Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

    //     if (Cnpj.Length != 14)
    //     {
    //         throw new Exception("CNPJ inválido");
    //     }

    //     tempCnpj = Cnpj.Substring(0, 12);
    //     soma = 0;

    //     for (int i = 0; i < 12; i++)
    //     {
    //         soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
    //     }

    //     resto = (soma % 11);

    //     if (resto < 2)
    //     {
    //         resto = 0;
    //     }
    //     else
    //     {
    //         resto = 11 - resto;
    //     }

    //     digito = resto.ToString();
    //     tempCnpj = tempCnpj + digito;
    //     soma = 0;

    //     for (int i = 0; i < 13; i++)
    //     {
    //         soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
    //     }

    //     resto = (soma % 11);

    //     if (resto < 2)
    //     {
    //         resto = 0;
    //     }
    //     else
    //     {
    //         resto = 11 - resto;
    //     }

    //     digito = digito + resto.ToString();

    //     if (!Cnpj.EndsWith(digito))
    //     {
    //         throw new Exception("CNPJ inválido");
    //     }
    //     else
    //     {
    //         throw new Exception("CNPJ válido");
    //     }
    // }


    public void ValidateOrder()
    {
        if (Orders.Any())
        {
            throw new Exception("Suplier não pode ter pedidos");
        }
    }
    public void ValidateGender()//faz sentido isso dessa forma?
    {
        if ((Cnpj != null || Cnpj != "") && (Gender != 0))//se tiver Cnpj e tiver um gênero ta errado
        {
            throw new Exception("Suplier com CNPJ não pode ter gênero");
        }
    }

    public void ValidateCpfOrCnpj()
    {
        if((Cnpj == null || Cnpj == "") && (Cpf == null || Cpf == ""))
        {
            throw new Exception("Suplier tem que ter CPF ou CNPJ");
        }
    }

    public void IsValid()
    {
        // ValidateCnpj();
        ValidateOrder();
        ValidateGender();
        ValidateCpfOrCnpj();
    }
}