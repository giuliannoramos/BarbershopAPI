using Barbearia.Domain.Entities;

namespace UnitTests.Barbearia.DomainTests;

public class TelephoneTestsFixture
{
    public Telephone GenerateValidTelephone()
    {
        return new Telephone()
        {
            Number = "47988887777",
            Type = Telephone.TelephoneType.Mobile,
            PersonId = 1
        };
    }

    public Telephone GenerateInvalidTelephone()
    {
        return new Telephone()
        {
            Number = "479888877776",
            Type = Telephone.TelephoneType.Mobile,
            PersonId = 1
        };
    }

}