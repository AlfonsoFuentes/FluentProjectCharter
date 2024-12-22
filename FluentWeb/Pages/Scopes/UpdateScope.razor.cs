using Microsoft.AspNetCore.Components;
using Shared.Models.Scopes.Records;
using Shared.Models.Scopes.Requests;
using Shared.Models.Scopes.Responses;
#nullable disable
namespace FluentWeb.Pages.Scopes;
public partial class UpdateScope
{
    UpdateScopeRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var result = await GenericService.GetById<ScopeResponse, GetScopeByIdRequest>(new GetScopeByIdRequest() { Id = Id, ProjectId= ProjectId });

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
