namespace Barbearia.Domain.Entities;

public class Suplier : Person
{
    // private void CheckCnpj()
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


    private void CheckOrder()
    {
        if (Orders.Any())
        {
            throw new Exception("Fornecedor não pode ter pedidos");
        }
    }

    private void CheckCpfOrCnpj()
    {
        if (string.IsNullOrWhiteSpace(Cpf) && string.IsNullOrWhiteSpace(Cnpj))
        {
            throw new Exception("Fornecedor deve ter CPF ou CNPJ");
        }
    }

    private void CheckGender()
    {
        if (!string.IsNullOrWhiteSpace(Cnpj) && Gender != 0)
        {
            throw new Exception("Fornecedor com CNPJ não pode ter gênero");
        }
    }

    public void ValidateSuplier()
    {
        //CheckCnpj();
        CheckOrder();
        CheckCpfOrCnpj();
        CheckGender();
    }
}