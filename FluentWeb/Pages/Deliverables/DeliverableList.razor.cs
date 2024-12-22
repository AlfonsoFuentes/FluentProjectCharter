using Microsoft.AspNetCore.Components;
using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Scopes.Responses;

namespace FluentWeb.Pages.Deliverables;
#nullable disable
public partial class DeliverableList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public ScopeResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


 
    public List<DeliverableResponse> Items => Parent == null ? new() : Parent.Deliverables;
    string nameFilter;
    public List<DeliverableResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateDeliverable/{Parent.Id}/{Parent.ProjectId}");

    }



    void Edit(DeliverableResponse response)
    {
        Navigation.NavigateTo($"/UpdateDeliverable/{response.Id}/{Parent.ProjectId}");
    }

    void CancelAsync()
    {

    }
    public async Task Delete(DeliverableResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteDeliverableRequest request = new()
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
