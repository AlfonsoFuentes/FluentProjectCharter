using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.EHSs.Records;
using Shared.Models.BudgetItems.EHSs.Requests;
using Shared.Models.BudgetItems.EHSs.Responses;

namespace FluentWeb.Pages.BudgetItems.EHSs;
public partial class UpdateEHS
{
    UpdateEHSRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<EHSResponse, GetEHSByIdRequest>(
            new GetEHSByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
                Quantity = result.Data.Quantity,
                UnitaryCost = result.Data.UnitaryCost,

            };
        }
    }
}
