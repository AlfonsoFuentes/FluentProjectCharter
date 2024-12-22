using Microsoft.AspNetCore.Components;
using Shared.Models.SucessfullFactors.Records;
using Shared.Models.SucessfullFactors.Requests;
using Shared.Models.SucessfullFactors.Responses;
#nullable disable
namespace FluentWeb.Pages.SucessfullFactors;
public partial class UpdateSucessfullFactor
{
    UpdateSucessfullFactorRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<SucessfullFactorResponse, GetSucessfullFactorByIdRequest>(
            new GetSucessfullFactorByIdRequest() { Id = Id, ProjectId = ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
            };
        }
    }
    private void CancelAsync()
    {
        Navigation.NavigateBack();

    }
}
