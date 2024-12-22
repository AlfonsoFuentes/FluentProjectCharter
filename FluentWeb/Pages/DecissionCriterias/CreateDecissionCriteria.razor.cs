using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.DecissionCriterias.Requests;
using static Shared.StaticClasses.StaticClass;
#nullable disable
namespace FluentWeb.Pages.DecissionCriterias;
public partial class CreateDecissionCriteria
{
    CreateDecissionCriteriaRequest Model = new();

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
