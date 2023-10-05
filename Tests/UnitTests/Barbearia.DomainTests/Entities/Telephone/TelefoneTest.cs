namespace UnitTests.Barbearia.DomainTests;
public class TelephoneTests : IClassFixture<TelephoneTestsFixture>
{
    private TelephoneTestsFixture _fixture;

    public TelephoneTests(TelephoneTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "New valid Telephone")]
    [Trait("Category", "Telephone Unit Tests")]
    public void CheckNumber_ValidTelephone()
    {
        // Arrange
        var Telephone = _fixture.GenerateValidTelephone();

        // Act        
        Telephone.ValidateTelephone();

        // Assert
        // Se não houver exception, o teste será bem-sucedido

    }

    [Fact(DisplayName = "New invalid Telephone")]
    [Trait("Category", "Telephone Unit Tests")]
    public void CheckNumber_InvalidTelephone()
    {
        // Arrange
        var Telephone = _fixture.GenerateInvalidTelephone();

        // Act
        Action act = () => Telephone.ValidateTelephone();

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact(DisplayName = "New valid Telephone bogus", Skip = "Not Implemented")]
    public void CheckAge_ValidTelephone_Bogus()
    {
        // asfjasjfoasjiof
        // ! Ainda não está corrigido
        // TODO: implementar bogus para preencher Telephone
        // ? Há necessidade de colocar bogus?
        // * Corrigido
    }
}