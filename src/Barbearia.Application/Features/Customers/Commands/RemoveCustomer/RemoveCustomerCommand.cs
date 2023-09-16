using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Barbearia.Application.Features.Customers.Commands.RemoveCustomer;

public class RemoveCustomerCommand : IRequest<bool>
{
    [Required(ErrorMessage = "You should fill an Id")]
    public int Id {get;set;}
}