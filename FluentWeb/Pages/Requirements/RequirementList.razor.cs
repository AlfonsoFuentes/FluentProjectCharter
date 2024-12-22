using Microsoft.AspNetCore.Components;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Requirements.Requests;
using Shared.Models.Requirements.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.Requirements;
#nullable disable
public partial class RequirementList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public DeliverableResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<RequirementResponse> Items => Parent == null ? new() : Parent.Requirements;
    string nameFilter;
    public List<RequirementResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateRequirement/{Parent.Id}/{Parent.ProjectId}");

    }

    void Edit(RequirementResponse response)
    {
        Navigation.NavigateTo($"/UpdateRequirement/{response.Id}/{Parent.ProjectId}");
    }
    public async Task Delete(RequirementResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteRequirementRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.Id,
            };
            var resultDelete = await Service.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll.Invoke();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }

}
