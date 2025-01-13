using Microsoft.AspNetCore.Components;
using Shared.Models.Brands.Records;
using Shared.Models.Brands.Requests;
using Shared.Models.Brands.Responses;
#nullable disable
namespace FluentWeb.Pages.Brands;
public partial class UpdateBrand
{
    UpdateBrandRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<BrandResponse, GetBrandByIdRequest>(
            new GetBrandByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
               
            };
        }
    }
   
}
