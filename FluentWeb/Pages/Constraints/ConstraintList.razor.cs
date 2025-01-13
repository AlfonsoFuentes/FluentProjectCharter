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
    public Guid DeliverableId { get; set; }
    [Parameter]
    [EditorRequired]
    public Guid ProjectId { get; set; }
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


    [Parameter]
    [EditorRequired]
    public List<ConstrainstResponse> Items { get; set; }
    string nameFilter;
    public List<ConstrainstResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateConstrainst/{DeliverableId}/{ProjectId}");

    }



    void Edit(ConstrainstResponse response)
    {
        Navigation.NavigateTo($"/UpdateConstrainst/{response.Id}/{ProjectId}");
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
                ProjectId = ProjectId,
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
