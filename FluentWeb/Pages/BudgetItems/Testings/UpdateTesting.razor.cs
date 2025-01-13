using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Testings.Records;
using Shared.Models.BudgetItems.Testings.Requests;
using Shared.Models.BudgetItems.Testings.Responses;

namespace FluentWeb.Pages.BudgetItems.Testings;
public partial class UpdateTesting
{
    UpdateTestingRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<TestingResponse, GetTestingByIdRequest>(
            new GetTestingByIdRequest() { Id = Id, ProjectId = ProjectId });

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
