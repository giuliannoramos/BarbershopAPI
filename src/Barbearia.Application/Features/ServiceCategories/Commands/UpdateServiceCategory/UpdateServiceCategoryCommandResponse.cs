namespace Barbearia.Application.Features.ServiceCategories.Commands.UpdateServiceCategory;

public class UpdateServiceCategoryCommandResponse : BaseResponse
{
    public UpdateServiceCategoryDto ServiceCategory{get;set;} = default!;
}