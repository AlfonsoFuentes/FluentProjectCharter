using Microsoft.AspNetCore.Components;
using Shared.Models.Backgrounds.Requests;
#nullable disable
namespace FluentWeb.Pages.BackGrounds;
public partial class CreateBackGround
{
    CreateBackGroundRequest Model = new();
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
