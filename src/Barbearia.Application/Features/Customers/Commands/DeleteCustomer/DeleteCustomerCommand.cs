using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Barbearia.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<bool>
{
    [Required(ErrorMessage = "You should fill an Id")]
    public int Id {get;set;}
}