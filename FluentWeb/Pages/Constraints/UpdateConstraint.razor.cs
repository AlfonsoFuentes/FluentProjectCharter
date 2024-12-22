using Microsoft.AspNetCore.Components;
using Shared.Models.Constrainsts.Records;
using Shared.Models.Constrainsts.Requests;
using Shared.Models.Constrainsts.Responses;
#nullable disable
namespace FluentWeb.Pages.Constraints;
public partial class UpdateConstraint
{
    UpdateConstrainstRequest Model = new();
   
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<ConstrainstResponse, GetConstrainstByIdRequest>(
            new GetConstrainstByIdRequest() { Id = Id, ProjectId= ProjectId });

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
