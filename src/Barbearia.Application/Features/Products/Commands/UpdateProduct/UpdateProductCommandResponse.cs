namespace Barbearia.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandResponse : BaseResponse
{
    public UpdateProductDto Product {get; set;} = default!;    
}