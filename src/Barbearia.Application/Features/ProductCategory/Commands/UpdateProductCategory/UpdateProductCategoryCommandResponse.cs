namespace Barbearia.Application.Features.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommandResponse : BaseResponse
{
    public UpdateProductCategoryDto ProductCategory{get; set;} = default!;    
}