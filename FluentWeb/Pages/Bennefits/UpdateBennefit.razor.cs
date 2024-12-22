using Microsoft.AspNetCore.Components;
using Shared.Models.Bennefits.Records;
using Shared.Models.Bennefits.Requests;
using Shared.Models.Bennefits.Responses;
#nullable disable
namespace FluentWeb.Pages.Bennefits;
public partial class UpdateBennefit
{
    UpdateBennefitRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<BennefitResponse, GetBennefitByIdRequest>(
            new GetBennefitByIdRequest() { Id = Id,ProjectId=ProjectId });

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
