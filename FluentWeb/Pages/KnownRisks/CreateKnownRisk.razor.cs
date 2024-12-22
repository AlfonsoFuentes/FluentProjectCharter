using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.KnownRisks.Requests;
using static Shared.StaticClasses.StaticClass;
#nullable disable
namespace FluentWeb.Pages.KnownRisks;
public partial class CreateKnownRisk
{
    CreateKnownRiskRequest Model = new();
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
