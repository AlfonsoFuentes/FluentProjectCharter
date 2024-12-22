using Microsoft.AspNetCore.Components;
using Shared.Models.Backgrounds.Records;
using Shared.Models.Backgrounds.Requests;
using Shared.Models.Backgrounds.Responses;
#nullable disable
namespace FluentWeb.Pages.BackGrounds;
public partial class UpdateBackGround
{
    UpdateBackGroundRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<BackGroundResponse, GetBackgroundByIdRequest>(
            new GetBackgroundByIdRequest() { Id = Id, ProjectId= ProjectId });

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
