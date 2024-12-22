using Microsoft.AspNetCore.Components;
using Shared.Models.OrganizationStrategies.Records;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Responses;
#nullable disable

namespace FluentWeb.Pages.OrganizationStrategys;
public partial class UpdateOrganizationStrategy
{
    [CascadingParameter]
    public App App { get; set; }
 
    UpdateOrganizationStrategyRequest Model { get; set; } = new();
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<OrganizationStrategyResponse, GetOrganizationStrategyByIdRequest>(new GetOrganizationStrategyByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
             
            };
        }
    }
    private void CancelAsync()
    {
        Navigation.NavigateBack();

    }

}
