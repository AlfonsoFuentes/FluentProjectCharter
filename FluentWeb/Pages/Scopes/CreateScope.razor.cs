using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.Scopes.Requests;
using static Shared.StaticClasses.StaticClass;
#nullable disable
namespace FluentWeb.Pages.Scopes;
public partial class CreateScope
{
    CreateScopeRequest Model = new();
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
