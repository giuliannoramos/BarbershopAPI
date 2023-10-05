using Barbearia.Domain.Entities;
using Xunit.Abstractions;

namespace UnitTests.Barbearia.DomainTests;

public class PaymentTests : IClassFixture<PaymentTestsFixture>
{
    private PaymentTestsFixture _fixture;
    readonly ITestOutputHelper _outputHelper;

    public PaymentTests(PaymentTestsFixture fixture, ITestOutputHelper outputHelper)
    {
        _fixture = fixture;
        _outputHelper = outputHelper;
    }

    [Fact(DisplayName = "new Valid Payment")]
    [Trait("Category", "Payment Entity Unit Tests")]
    public void IsValid_WhenPaymentIsValid_ShouldReturnTrueAndHaveNoErrors()
    {
        //Arrange
        var payment = _fixture.GenerateValidPayment();

        //Act
        Action act = () => payment.ValidatePayment();


        //Assert


    }

    //[Fact(DisplayName = "new Valid Customer Bogus")]
    //[Trait("Category", "Customer Entity Unit Tests")]
    //public void IsValid_WhenCustomerIsValid_ShouldReturnTrueAndHaveNoErrors_Bogus()
    //{
    //    //Arrange
    //    var customer = _fixture.GenerateValidCustomerBogus();

    //    //Act
    //    var validationResult = customer.IsValid();

    //    //Assert
    //    Assert.True(validationResult);
    //    Assert.Empty(customer.ValidationResult.Errors);
    //}

    //[Fact(DisplayName = "new InValid Customer Bogus")]
    //[Trait("Category", "Customer Entity Unit Tests")]
    //public void IsValid_WhenCustomerIsNotOfAge_ShouldReturnFalseAndHaveErrors_Bogus()
    //{
    //    //Arrange
    //    var customer = _fixture.GenerateInvalidCustomerBogus();

    //    //Act
    //    var validationResult = customer.IsValid();

    //    //Assert
    //    validationResult.Should().BeFalse();
    //    customer.ValidationResult.Errors.Should().HaveCountGreaterThanOrEqualTo(1);

    //    _outputHelper.WriteLine($"Error found{customer.ValidationResult.Errors.FirstOrDefault()}");
    //}
}

