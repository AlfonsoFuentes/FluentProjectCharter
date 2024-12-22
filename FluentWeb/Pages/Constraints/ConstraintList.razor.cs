using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Constrainsts.Requests;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.Deliverables.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.Constraints;
#nullable disable
public partial class ConstraintList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public DeliverableResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


 
    public List<ConstrainstResponse> Items => Parent == null ? new() : Parent.Constrainsts;
    string nameFilter;
    public List<ConstrainstResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateConstrainst/{Parent.Id}/{Parent.ProjectId}");

    }



    void Edit(ConstrainstResponse response)
    {
        Navigation.NavigateTo($"/UpdateConstrainst/{response.Id}/{Parent.ProjectId}");
    }
    public async Task Delete(ConstrainstResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteConstrainstRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.Id,
            };
            var resultDelete = await GenericService.Delete(request);
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
