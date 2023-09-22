using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Barbearia.Domain.Entities;

public class Order
{
    public int OrderId{get; set;}
    public int Number {get;set;}
    public int Status{get;set;}
    public DateTime BuyDate{get;set;}
    public int PersonId { get; set; }
    public Person? Person { get; set; } 
    public Payment? Payment{get; set; }

    //no banco, order está com a id de payment, mas não é necessário,
    //pois payment já tem o id de order. Além disso, a relação é opcional.

    bool IsNumberValid()
    {
        if(Number<=0)return false;

        //quando for criado o repositorio, tem que adicionar uma checagem aqui se o numero ja existe no banco

        return true;
    }

    bool IsCustomerValid()
    {
        if(Person==null) return false;

        return true;
    }

    bool IsValid()
    {
        if(!IsNumberValid()) return false;

        if(!IsCustomerValid()) return false;

        return true;
    }


}