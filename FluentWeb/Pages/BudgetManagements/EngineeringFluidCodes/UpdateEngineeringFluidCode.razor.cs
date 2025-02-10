using Microsoft.AspNetCore.Components;
using Shared.Models.EngineeringFluidCodes.Records;
using Shared.Models.EngineeringFluidCodes.Requests;
using Shared.Models.EngineeringFluidCodes.Responses;

#nullable disable

namespace FluentWeb.Pages.BudgetManagements.EngineeringFluidCodes;
public partial class UpdateEngineeringFluidCode
{
    [CascadingParameter]
    public App App { get; set; }

    UpdateEngineeringFluidCodeRequest Model { get; set; } = new();
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<EngineeringFluidCodeResponse, GetEngineeringFluidCodeByIdRequest>(new GetEngineeringFluidCodeByIdRequest() { Id = Id });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Code = result.Data.Code,

            };
        }
    }
    private void CancelAsync()
    {
        Navigation.NavigateBack();

    }

}
