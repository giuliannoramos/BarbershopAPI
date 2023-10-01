namespace Barbearia.Application.Features.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierCommandResponse : BaseResponse
{
    public UpdateSupplierDto Supplier {get; set;} = default!;    
}