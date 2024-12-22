using Microsoft.AspNetCore.Components;
using Shared.Models.SucessfullFactors.Requests;
#nullable disable
namespace FluentWeb.Pages.SucessfullFactors;
public partial class CreateSucessfullFactor
{
    CreateSucessfullFactorRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid CaseId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.CaseId = CaseId;
    }
    private void CancelAsync()
    {
        Navigation.NavigateBack();

    }
}
